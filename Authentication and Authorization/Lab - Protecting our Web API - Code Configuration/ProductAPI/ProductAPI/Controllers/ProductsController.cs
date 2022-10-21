using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.Services;

namespace ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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

        
    }
}
