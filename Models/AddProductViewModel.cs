using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace p48_lessons_asp.Models;

public class AddProductViewModel
{
    public string ProductName{get;set;}
    public SelectList? Products{get;set;}
}
