using AutoMapper;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using SoftwareFullComponents.Product2Component.DTO;
using SoftwareFullComponents.Product2Component.Models;

namespace SoftwareFullComponents.Product2Component.Profiles
{
    public class ProductProfile: Profile
    {
        [ExcludeFromCodeCoverage]
        public ProductProfile()
        {
            CreateMap<Product, ProductRead>();
            CreateMap<ProductCreate, Product>();
            CreateMap<ProductEdit, Product>();
        }
    }
}
