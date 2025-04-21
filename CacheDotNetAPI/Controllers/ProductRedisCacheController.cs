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

        [HttpGet]
        public Task<ResponseProductModel?> GetProduct(string productCode = "all")
        {
            var response = _productService.GetProduct(productCode);
            return response;
        }

        [HttpGet]
        public Task<ResponseModel> ClearCache(string productCode = "all")
        {
            var response = _productService.ClearCache(productCode);
            return response;
        }

        //[HttpGet]
        //public Task<string> TestSet()
        //{
        //    var response = _productService.Set();
        //    return response;
        //}

        //[HttpGet]
        //public async Task<string?> TestGet()
        //{
        //    var response = await _productService.Get();
        //    return response;
        //}

        //[HttpGet]
        //public async Task<string> TestDelete()
        //{
        //    var response = await _productService.Delete();
        //    return response;
        //}
    }
}
