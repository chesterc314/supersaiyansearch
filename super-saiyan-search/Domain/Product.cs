using SuperSaiyanSearch.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
namespace SuperSaiyanSearch.Domain
{
    public class Product : IProduct
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public decimal Price { get; set; }
        public int Units { get; set; }
        public string Source { get; set; }
        public string SourceUrl { get; set; }
        public string ImageUrl { get; set; }
        public int Order { get; set; }

        public Product()
        {

        }
        public Product(IProduct product)
        {
            this.Name = product.Name;
            this.Description = product.Description;
            this.Brand = product.Brand;
            this.Price = product.Price;
            this.Source = product.Source;
            this.SourceUrl = product.SourceUrl;
            this.ImageUrl = product.ImageUrl;
            this.Order = product.Order;
        }
    }
}
