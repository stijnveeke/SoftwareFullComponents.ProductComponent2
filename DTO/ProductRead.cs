﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace SoftwareFullComponents.Product2Component.DTO
{
    [ExcludeFromCodeCoverage]
    public class ProductRead
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public string ProductName { get; set; }
        public string ProductSlug { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string MetaTags { get; set; }
    }
}
