using Microsoft.EntityFrameworkCore;

namespace CacheDotNetAPI.DataAccess.Interfaces
{
    public interface IProductDataSet
    {
        DbSet<ProductEntity> productEntity { get; }
    }
}
