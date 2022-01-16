﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace SoftwareFullComponents.Product2Component.DTO
{
    [ExcludeFromCodeCoverage]
    public class ProductEdit
    {
        public int Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string ProductSlug { get; set; }
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        public string MetaTags { get; set; }
    }
}
