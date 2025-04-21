using CacheDotNetAPI.DataAccess;

namespace CacheDotNetAPI.Models
{
    public class RequestProductModel
    {
        public string productCode { get; set; } = string.Empty;
    }

    public class ResponseProductModel : ResponseModel
    {
        public List<Product> data { get; set; } = new List<Product>();
    }

    public class ResponseModel
    {
        public int status { get; set; } = 500;
        public bool success { get; set; }
        public string message { get; set; } = string.Empty;
        public object? error { get; set; }
    }

    public class Product : ProductEntity
    {
        
    }
}
