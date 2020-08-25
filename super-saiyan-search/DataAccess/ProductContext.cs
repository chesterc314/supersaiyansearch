using Microsoft.EntityFrameworkCore;
using SuperSaiyanSearch.Domain;

namespace SuperSaiyanSearch.DataAccess
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> opt) : base(opt)
        {

        }
        public DbSet<Product> Products { get; set; }
    }
}
