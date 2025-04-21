using CacheDotNetAPI.Models;
using CacheDotNetAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CacheDotNetAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductRedisCacheController : ControllerBase
    {
        private readonly ProductRedisCacheService _productService;

        public ProductRedisCacheController(ProductRedisCacheService productService)
        {
            _productService = productService;
        }

        [HttpGet("{productCode}")]
        public Task<ResponseProductModel?> GetProduct(string productCode = "all")
        {
            var response = _productService.GetProduct(productCode);
            return response;
        }

        [HttpGet("{productCode}")]
        public Task<ResponseModel> ClearCache(string productCode = "all")
        {
            var response = _productService.ClearCache(productCode);
            return response;
        }
    }
}
