using NorthwindRepository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindRepository
{
    public interface IProductRepository
    {
        IEnumerable<ProductOutputModelForRepo> GetProducts();
        ProductOutputModelForRepo GetProduct(int productId);
        bool CreateProduct(ProductInputModelForRepo productInputModelForRepo);
        bool UpdateProduct(int productId, ProductInputModelForRepo productInputModelForRepo);
        bool DeleteProduct(int productId);
    }
}
