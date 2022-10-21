using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.Models;
using ProductAPI.Services;

namespace ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService productService;

        public ProductsController()
        {
            productService = new ProductService();
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            return Ok(productService.GetProducts());
        }


        [HttpGet("{id}")]
        public IActionResult GetProduct(string id)
        {
            return Ok(productService.GetProduct(id));
        }

        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            int rowsAffected=productService.AddProduct(product);
            if (rowsAffected>0)
                return Ok("Added");
            else return Ok("Error occured");
        }

    }
}
