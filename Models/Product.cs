using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace SoftwareFullComponents.Product2Component.Models
{
    [ExcludeFromCodeCoverage]
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Guid { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string ProductName { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string ProductSlug { get; set; }
        [Column(TypeName = "text")]
        public string Description { get; set; }
        [Column(TypeName = "decimal(4,2)")]
        public double Price { get; set; }
        public string MetaTags { get; set; }
    }
}
