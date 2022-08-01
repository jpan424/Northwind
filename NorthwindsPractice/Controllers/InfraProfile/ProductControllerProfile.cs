using AutoMapper;
using NorthwindService.Models;
using NorthwindsPractice.Controllers.Models;

namespace NorthwindsPractice.Controllers.InfraProfile
{
    public class ProductControllerProfile:Profile
    {
        public ProductControllerProfile()
        {
            CreateMap<ProductInputModelForController, ProductInputModelForService>();
            CreateMap<ProductOutputModelForService, ProductOutputModelForController>();
        }
    }
}
