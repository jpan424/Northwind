using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NorthwindService;
using NorthwindService.Models;
using NorthwindsPractice.Controllers.Models;
using System.Collections.Generic;

namespace NorthwindsPractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            var productsForService = _productService.GetProducts();
            var result = _mapper.Map<IEnumerable<ProductOutputModelForController>>(productsForService);
            return Ok(new ProductResponseModel<IEnumerable<ProductOutputModelForController>> { Data = result });

        }
        [HttpGet("{productId}")]
        public IActionResult GetProduct(int productId)
        {
            var productForService = _productService.GetProduct(productId);
            var result = _mapper.Map<ProductOutputModelForController>(productForService);
            return Ok(new ProductResponseModel<ProductOutputModelForController> { Data = result });
        }
        [HttpPost]
        public IActionResult CreateProduct(ProductInputModelForController productInputModelForController)
        {
            var productForService = _mapper.Map<ProductInputModelForService>(productInputModelForController);
            var result = _productService.CreateProduct(productForService);
            if (!result)
            {
                return BadRequest(new ProductResponseModel<ProductOutputModelForController> { Message="新增失敗"});
            }
            return Ok(new ProductResponseModel<ProductOutputModelForController> { Message = "新增成功" });
        }
        [HttpPut]
        public IActionResult UpdateProduct(int productId, ProductInputModelForController productInputModelForController)
        {
            var productForService = _mapper.Map<ProductInputModelForService>(productInputModelForController);
            var result = _productService.UpdateProduct(productId, productForService);
            if (!result)
            {
                return BadRequest(new ProductResponseModel<ProductOutputModelForController> { Message = "更新失敗" });
            }
            return Ok(new ProductResponseModel<ProductOutputModelForController> { Message = "更新成功" });
        }
        [HttpDelete]
        public IActionResult DeleteProduct(int productId)
        {
            var result = _productService.DeleteProduct(productId);
            if (!result)
            {
                return BadRequest(new ProductResponseModel<ProductOutputModelForController> { Message = "刪除失敗" });
            }
            return Ok(new ProductResponseModel<ProductOutputModelForController> { Message = "刪除成功" });
        }
    }
}
