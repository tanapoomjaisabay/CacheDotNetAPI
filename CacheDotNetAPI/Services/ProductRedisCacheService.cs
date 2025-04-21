using System.Text.Json;
using CacheDotNetAPI.DataAccess;
using CacheDotNetAPI.Models;
using CacheDotNetAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace CacheDotNetAPI.Services
{
    public class ProductRedisCacheService
    {
        private readonly IDatabase cached;
        private readonly ProductContext context;
        private readonly IConfiguration config;

        public ProductRedisCacheService(ProductContext context, IConnectionMultiplexer connectionMultiplexer, IConfiguration config)
        {
            this.context = context;
            cached = connectionMultiplexer.GetDatabase();
            this.config = config;
        }

        public async Task<ResponseProductModel?> GetProduct(string productCode = "all")
        {
            string redisKey = $"Product_{productCode}";

            try
            {
                var cachedProduct = await cached.StringGetAsync(redisKey);
                if (cachedProduct.IsNullOrEmpty)
                {
                    var result = (from tb in context.productEntity
                                  where (productCode.Trim() == "all" ? true : tb.productCode.Trim() == productCode.Trim())
                                  select tb).ToList();

                    var listData = JsonSerializer.Deserialize<List<Product>>(JsonSerializer.Serialize(result));
                    if (listData == null)
                    {
                        throw new Exception("Not Found Data");
                    }

                    var setResult = await cached.StringSetAsync(redisKey, JsonSerializer.Serialize(listData), TimeSpan.FromMinutes(Convert.ToInt32(config["CacheExpireMin"])));
                    if (!setResult)
                    {
                        throw new Exception("Error Set String");
                    }

                    return new ResponseProductModel
                    {
                        status = 200,
                        success = true,
                        data = listData
                    };
                }
                else
                {
                    var listData = JsonSerializer.Deserialize<List<Product>>(cachedProduct);
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
                }
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

        public async Task<ResponseModel> ClearCache(string productCode = "all")
        {
            try
            {
                string redisKey = $"Product_{productCode}";
                var value = await cached.KeyDeleteAsync(redisKey);
                return new ResponseModel 
                {
                    status = 200,
                    success = value
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

        public async Task<string> Set()
        {
            await cached.StringSetAsync("testKey", "Hello Redis " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), TimeSpan.FromMinutes(1));
            return "OK";
        }

        public async Task<string?> Get()
        {
            var value = await cached.StringGetAsync("testKey");
            return value;
        }

        public async Task<string> Delete()
        {
            var value = await cached.KeyDeleteAsync("testKey");
            return "OK";
        }
    }
}
