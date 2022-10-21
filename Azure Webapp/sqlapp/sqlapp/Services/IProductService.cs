using sqlapp.models;

namespace sqlapp.Services
{
    public interface IProductService
    {
        /*<*/List<Product>/*>*/ GetProdcuts();

        Task<bool> IsBeta();
    }
}