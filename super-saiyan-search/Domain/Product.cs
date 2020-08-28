using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SuperSaiyanSearch.Domain.Interfaces;

namespace SuperSaiyanSearch.Domain
{
    public class Product : IEntity<Guid>, IProduct
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public string Brand { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        public int Units { get; set; }
        [Required]
        public string Source { get; set; }
        [Required]
        public string SourceUrl { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Product()
        {
            this.CreatedDate = DateTime.UtcNow;
            this.Id = Guid.NewGuid();
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
            this.CreatedDate = DateTime.UtcNow;
        }
    }
}
