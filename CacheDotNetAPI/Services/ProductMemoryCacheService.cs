using System;
using System.Text.Json;
using CacheDotNetAPI.DataAccess;
using CacheDotNetAPI.Models;
using CacheDotNetAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace CacheDotNetAPI.Services
{
    public class ProductMemoryCacheService
    {
        private readonly ProductContext context;
        private readonly IMemoryCache memoryCache;
        private readonly IConfiguration config;

        public ProductMemoryCacheService(ProductContext context, IMemoryCache memoryCache, IConfiguration config)
        {
            this.context = context;
            this.memoryCache = memoryCache;
            this.config = config;
        }

        public ResponseProductModel? GetProduct(string productCode = "all")
        {
            try
            {
                return memoryCache.GetOrCreate($"Product_{productCode}", entry =>
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(Convert.ToInt32(config["CacheExpireMin"]));

                    var result = (from tb in context.productEntity
                                  where (productCode.Trim() == "all" ? true : tb.productCode.Trim() == productCode.Trim())
                                  select tb).ToList();

                    var listData = JsonSerializer.Deserialize<List<Product>>(JsonSerializer.Serialize(result));
                    if (listData == null)
                    {
                        throw new Exception("Not Found Data");
                    }

                    return new ResponseProductModel
                    {
                        status = 200,
                        success = true,
                        data = listData
                    };
                });
            }
            catch (Exception ex)
            {
                return new ResponseProductModel
                {
                    status = 500,
                    message = ex.Message
                };
            }
        }

        public ResponseModel ClearCache(string productCode = "all")
        {
            try
            {
                memoryCache.Remove($"Product_{productCode}");

                return new ResponseProductModel
                {
                    status = 200,
                    success = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseProductModel
                {
                    status = 500,
                    message = ex.Message
                };
            }
        }
    }
}
