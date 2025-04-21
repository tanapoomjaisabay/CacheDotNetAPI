namespace CacheDotNetAPI.DataAccess
{
    public class ProductEntity
    {
        public int idKey { get; set; }
        public string productCode { get; set; } = string.Empty;
        public string productName { get; set; } = string.Empty;
        public string? productDesc { get; set; }
        public decimal price { get; set; }
    }
}
