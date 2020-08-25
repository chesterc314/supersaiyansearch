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
        [Required]
        public string Brand { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        public int Units { get; set; }
        [Required]
        public string Source { get; set; }
        [Required]
        public string SourceUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
    }
}
