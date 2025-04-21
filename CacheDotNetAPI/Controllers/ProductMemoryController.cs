using CacheDotNetAPI.Services.Interfaces;
using CacheDotNetAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CacheDotNetAPI.Models;

namespace CacheDotNetAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductMemoryController : ControllerBase
    {
        private readonly ProductMemoryCacheService _productService;

        public ProductMemoryController(ProductMemoryCacheService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public ResponseProductModel? GetProduct(string productCode = "all")
        {
            var response = _productService.GetProduct(productCode);
            return response;
        }

        [HttpGet]
        public ResponseModel ClearCacheAsync(string productCode = "all")
        {
            var response = _productService.ClearCache(productCode);
            return response;
        }
    }
}
