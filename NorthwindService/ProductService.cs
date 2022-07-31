using AutoMapper;
using NorthwindRepository;
using NorthwindRepository.Models;
using NorthwindService.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NorthwindService
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductService(IProductRepository productRepository,IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public bool CreateProduct(ProductInputModelForService productInputModelForRepo)
        {
            var productForCreate = _mapper.Map<ProductInputModelForRepo>(productInputModelForRepo);

            productForCreate.UnitPrice = Division(productForCreate.UnitPrice);//匯率轉換 台幣=>美金

            var result = _productRepository.CreateProduct(productForCreate);
            if (!result)
            {
                return false;
            }
            return true;
        }

        public bool DeleteProduct(int productId)
        {
            var result = _productRepository.DeleteProduct(productId);
            if (!result)
            {
                return false;
            }
            return true;
        }

        public ProductOutputModelForService GetProduct(int productId)
        {
            var productForRepo = _productRepository.GetProduct(productId);

            productForRepo.UnitPrice = Mutiple(productForRepo.UnitPrice);//匯率轉換 美金=>台幣

            var result = _mapper.Map<ProductOutputModelForService>(productForRepo);
            return result;
        }

        public IEnumerable<ProductOutputModelForService> GetProducts()
        {
            var productsForRepo = _productRepository.GetProducts();

            productsForRepo.ToList().ForEach(x => x.UnitPrice = Mutiple(x.UnitPrice));//逐一取出做匯率轉換

            var result = _mapper.Map<IEnumerable<ProductOutputModelForService>>(productsForRepo);
            return result;
        }

        public bool UpdateProduct(int productId, ProductInputModelForService productInputModelForRepo)
        {
            var productForUpdate = _mapper.Map<ProductInputModelForRepo>(productInputModelForRepo);
            productForUpdate.UnitPrice = Division(productForUpdate.UnitPrice);
            var result = _productRepository.UpdateProduct(productId, productForUpdate);
            if (!result)
            {
                return false;
            }
            return true;
        }
        public decimal Division(decimal unitPrice)
        {
            return unitPrice /= 30;
        }
        public decimal Mutiple(decimal unitPrice)
        {
            return unitPrice *= 30;
        }
    }
}
