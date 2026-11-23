using System;
using p48_lessons_asp.Models;

namespace p48_lessons_asp.Services.ShoppingCartService;

public interface IShoppingCartService
{
    void AddProduct(ProductListItemModel product);
    List<ProductListItemModel> GetProducts();
}
