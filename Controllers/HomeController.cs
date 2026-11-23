using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using p48_lessons_asp.Models;
using p48_lessons_asp.Services.Operation;
using p48_lessons_asp.Services.RequestLogger;

namespace p48_lessons_asp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOperation _transient1;

        private readonly IOperation _transient2;

        private readonly IOperation _scoped1;
        private readonly IOperation _scoped2;

        private readonly IOperation _singleton1;
        private readonly IOperation _singleton2;

        private readonly IRequestLogger _requestLogger;

        public HomeController(
        [FromKeyedServices("transient")] IOperation transient1,
        [FromKeyedServices("transient")] IOperation transient2,
        [FromKeyedServices("scoped")] IOperation scoped1,
        [FromKeyedServices("scoped")] IOperation scoped2,
        [FromKeyedServices("singleton")] IOperation singleton1,
        [FromKeyedServices("singleton")] IOperation singleton2
,
        IRequestLogger requestLogger)
        {
            _transient1 = transient1;
            _transient2 = transient2;
            _scoped1 = scoped1;
            _scoped2 = scoped2;
            _singleton1 = singleton1;
            _singleton2 = singleton2;
            _requestLogger = requestLogger;
        }
        public IActionResult UserPage()
        {
            _requestLogger.LogRequest(HttpContext.Request.Method, HttpContext.Request.GetDisplayUrl() ,StatusCodes.Status200OK);
            return View("Index");
        }

        public IActionResult Service()
        {
            ViewBag.Transient1 = _transient1.OperationId;
            ViewBag.Transient2 = _transient2.OperationId;

            ViewBag.Scoped1 = _scoped1.OperationId;
            ViewBag.Scoped2 = _scoped2.OperationId;

            ViewBag.Singleton1 = _singleton1.OperationId;
            ViewBag.Singleton2 = _singleton2.OperationId;
            _requestLogger.LogRequest(HttpContext.Request.Method, HttpContext.Request.GetDisplayUrl() , StatusCodes.Status200OK);
            return View();
        }

        // action
        public IActionResult Index(int? id)
        {

            // Словник, який можна використовувати для передачі інформації на сторінку, має синтаксис та методи звичайного словника
            ViewData["Message"] = "Welcome to ASP.NET Core MVC";
            // Синтаксичний цукор для ViewData може містити dynamic props що не потребує приводити тип
            ViewBag.User = new
            {
                Name = "Petro",
                Surname = "Ivanov",
                Age = 20
            };
            if (id.HasValue)
            {
                System.Console.WriteLine($"ID: {id.Value}");
            }
            _requestLogger.LogRequest(HttpContext.Request.Method, HttpContext.Request.GetDisplayUrl() , StatusCodes.Status200OK);
            return View();
        }

        public IActionResult Product()
        {
            var product = new ProductModel
            {
                Name = "LG Monitor",
                Price = 100
            };
            _requestLogger.LogRequest(HttpContext.Request.Method, HttpContext.Request.GetDisplayUrl() , StatusCodes.Status200OK);
            return View(product);
        }

        public IActionResult SecondPage()
        {
            _requestLogger.LogRequest(HttpContext.Request.Method, HttpContext.Request.GetDisplayUrl() , StatusCodes.Status200OK);
            return View();
        }

        public IActionResult Save()
        {
            TempData["TempMessage"] = "This a temporary message";
            _requestLogger.LogRequest(HttpContext.Request.Method, HttpContext.Request.GetDisplayUrl() , StatusCodes.Status200OK);
            return RedirectToAction("Index");
        }
    }
}