using sqlapp.Models;

namespace sqlapp.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetProducts();
    }
}