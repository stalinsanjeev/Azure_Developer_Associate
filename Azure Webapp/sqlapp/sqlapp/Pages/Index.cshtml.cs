using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using sqlapp.models;
using sqlapp.Services;

namespace sqlapp.Pages;

public class IndexModel : PageModel
{
    public List<Product> Products;
    public bool Isbeta;
    private readonly IProductService _productService;

    public IndexModel(IProductService productService)
    {
        _productService = productService;
    }
    public void OnGet()
    {
        Isbeta = _productService.IsBeta().Result;
        Products = _productService.GetProdcuts();// for azure functions .GetAwaiter().GetResult();
    }
}

