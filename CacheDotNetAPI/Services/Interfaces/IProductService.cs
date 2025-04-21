using CacheDotNetAPI.Models;

namespace CacheDotNetAPI.Services.Interfaces
{
    public interface IProductService
    {
        ResponseProductModel? GetProduct(string productCode = "");
        ResponseModel ClearCache(string productCode = "");
    }
}
