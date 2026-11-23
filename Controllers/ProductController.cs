using System.Text.Json;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using p48_lessons_asp.Models;
using p48_lessons_asp.Services.RequestLogger;
using p48_lessons_asp.Services.ShoppingCartService;

namespace p48_lessons_asp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IRequestLogger _requestLogger;
        private readonly IShoppingCartService _shoppingCartService;

        public ProductController(IRequestLogger requestLogger, IShoppingCartService shoppingCartService)
        {
            _requestLogger = requestLogger;
            _shoppingCartService = shoppingCartService;
        }

        // DbContext
        private static List<ProductListItemModel> _products = new()
        {
            new() {Name="Macbook Pro M4", Price = 90000, CategoryName="Most expensive gadgets(Apple)"},
            new() {Name="Samsung Washmachine", Price = 30000, CategoryName = "Appliances"}
        };

        private static Dictionary<Guid, string> _categories = new (){
            {Guid.Parse("ef2e3af3-ee40-4351-be38-3231b1505a96"), "Most expensive gadgets(Apple)"},
            {Guid.Parse("11ba17ad-3ef4-49b2-8a24-194ac2db596f"), "Appliances"},
            {Guid.Parse("afe51c95-3926-40c8-8fb3-3374c447ade6"),"Clothes"}
        };

        [HttpGet]
        public IActionResult Index()
        {
            _requestLogger.LogRequest(HttpContext.Request.Method, HttpContext.Request.GetDisplayUrl() ,StatusCodes.Status200OK);
            return View(_products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var viewModel = new CreateProductViewModel
            {
                Categories = new(_categories, "Key", "Value")
            };
            _requestLogger.LogRequest(HttpContext.Request.Method, HttpContext.Request.GetDisplayUrl() , StatusCodes.Status200OK);
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateProductViewModel model)
        {

            /*
            CSRF-атака(Cross-Site-Request-Forgery) - суть атаки наступна:
            Користувач залогінився на вашому сайті, для нього сформувався токен, який зберігається у кукі. Є ризик що цим кукі може скористуватися інший ресурс, який може бути шкідливими, цей ресурс, зможе спробувати використати ваш кукі, щоб відправити запит на сайт в якому у вас є авторизація.
            */
            if (!ModelState.IsValid)
            {
                model.Categories = new(_categories, "Key","Value", model.CategoryId);
                _requestLogger.LogRequest(HttpContext.Request.Method, HttpContext.Request.GetDisplayUrl() , StatusCodes.Status400BadRequest);
                return View(model);
            }

            _products.Add(new ProductListItemModel
            {
                Name = model.Name,
                Price = model.Price,
                CategoryName = _categories[model.CategoryId]
            });
            _requestLogger.LogRequest(HttpContext.Request.Method, HttpContext.Request.GetDisplayUrl() , StatusCodes.Status201Created);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult TestShoppingCart()
        {
            _shoppingCartService.AddProduct(_products[1]);
            _shoppingCartService.AddProduct(_products[0]);
            var products = _shoppingCartService.GetProducts();
            return View(products);
        }
    }
}
