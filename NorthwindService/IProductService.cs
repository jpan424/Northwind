using NorthwindService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindService
{
    public interface IProductService
    {
        IEnumerable<ProductOutputModelForService> GetProducts();
        ProductOutputModelForService GetProduct(int productId);
        bool CreateProduct(ProductInputModelForService productInputModelForRepo);
        bool UpdateProduct(int productId, ProductInputModelForService productInputModelForRepo);
        bool DeleteProduct(int productId);
    }
}
