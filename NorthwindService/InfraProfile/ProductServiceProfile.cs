using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using NorthwindRepository.Models;
using NorthwindService.Models;

namespace NorthwindService.InfraProfile
{
    public class ProductServiceProfile:Profile
    {
        public ProductServiceProfile()
        {
            CreateMap<ProductOutputModelForRepo, ProductOutputModelForService>();
            CreateMap<ProductInputModelForService, ProductInputModelForRepo>();
        }
    }
}
