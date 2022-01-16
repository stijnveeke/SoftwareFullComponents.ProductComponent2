using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SoftwareFullComponents.Product2Component.Models;

namespace SoftwareFullComponents.Product2Component.Data
{
    public class ProductComponentContext : DbContext
    {
        public ProductComponentContext (DbContextOptions<ProductComponentContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Product { get; set; }
    }
}
