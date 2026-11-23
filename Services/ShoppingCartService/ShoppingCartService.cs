using System;
using p48_lessons_asp.Models;

namespace p48_lessons_asp.Services.ShoppingCartService;

public class ShoppingCartService : IShoppingCartService
{
    private List<ProductListItemModel> products = new();

    public void AddProduct(ProductListItemModel product)
    {
        var boolean = products.Any(p => p.Id == product.Id);
        if (!boolean)
        {
            products.Add(product);
        }
    }

    public List<ProductListItemModel> GetProducts()
    {
        return products;
    }
}
