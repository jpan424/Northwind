using AutoFixture;
using AutoMapper;
using FluentAssertions;
using NorthwindRepository;
using NorthwindRepository.Models;
using NorthwindService;
using NorthwindService.InfraProfile;
using NorthwindService.Models;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;

namespace NorthwindsUTs
{
    public class Tests
    {
        private IProductRepository _productRepository;
        private IMapper _mapper
        {
            get
            {
                var config = new MapperConfiguration(options => { options.AddProfile<ProductServiceProfile>(); });
                return config.CreateMapper();
            }
        }
        [SetUp]
        public void Setup()
        {
            this._productRepository = Substitute.For<IProductRepository>();
        }
        private ProductService InitializeSystemUnderTest()
        {
            return new ProductService(this._productRepository, this._mapper);
        }

        [Test]
        public void GetProduct()
        {
            //Arrange
            var fixture = new Fixture();
            var dataModel = fixture.Build<ProductOutputModelForRepo>()
                .With(x => x.ProductID, 1)
                .Create();

            this._productRepository.GetProduct(1).Returns(dataModel);
            var product = this._mapper.Map<ProductOutputModelForService>(dataModel);
            product.UnitPrice *= 30;

            //Act 
            var sub = InitializeSystemUnderTest();
            var expect = sub.GetProduct(1);

            //Assert 
            expect.Should().BeEquivalentTo(product);
        }

        [Test]
        public void GetProducts()
        {
            //Arrange
            var fixture = new Fixture();
            var dataModels = fixture.Build<ProductOutputModelForRepo>()
                .CreateMany();

            var products = _mapper.Map<IEnumerable<ProductOutputModelForService>>(dataModels);

            foreach (var i in products)
            {
                i.UnitPrice *= 30;
            }



            this._productRepository.GetProducts().Returns(dataModels);

            //Act 
            var sub = InitializeSystemUnderTest();
            var expect = sub.GetProducts();

            //Assert 
            expect.Should().BeEquivalentTo(products);
        }

        [Test]
        public void GetProduct_UnitPrice_ShouldBe_Mutiple30()
        {
            var sub = InitializeSystemUnderTest();

            var expect = sub.Mutiple(3);

            expect.Should().Be(90);
        }

        [Test]
        public void CreateProduct_ShouldBe_True()
        {
            //arrange

            var fixture = new Fixture();
            var productParameterModel = fixture.Build<ProductInputModelForService>()
                .Create();
            var productConditionModel = this._mapper.Map<ProductInputModelForRepo>(productParameterModel);
            //先設定return結果
            _productRepository.CreateProduct(productConditionModel).ReturnsForAnyArgs(true);

            var sub = new ProductService(_productRepository, _mapper);

            var expect = sub.CreateProduct(productParameterModel);

            expect.Should().BeTrue();
        }

        [Test]
        public void CreateProduct_ShouldBe_False()
        {
            //arrange
            var fixture = new Fixture();
            var productParameterModel = fixture.Build<ProductInputModelForService>()
                .Create();

            var productConditionModel = this._mapper.Map<ProductInputModelForRepo>(productParameterModel);
            //先設定return結果
            _productRepository.CreateProduct(productConditionModel).ReturnsForAnyArgs(false);

            var sub = new ProductService(_productRepository, _mapper);

            var expect = sub.CreateProduct(productParameterModel);

            expect.Should().BeFalse();
        }

        [Test]
        public void CreateProduct_UnitPrice_ShouldBe_Division30()
        {
            var sub = new ProductService(_productRepository, _mapper);

            var expect = sub.Division(300);

            expect.Should().Be(10);
        }

        [Test]
        public void UpdateProduct_ShouldBe_True()
        {
            //arrange
            var fixture = new Fixture();
            var productParameterModel = fixture.Build<ProductInputModelForService>()
                .Create();

            var productConditionModel = this._mapper.Map<ProductInputModelForRepo>(productParameterModel);

            _productRepository.UpdateProduct(1, productConditionModel).ReturnsForAnyArgs(true);

            var sub = new ProductService(_productRepository, _mapper);

            var expect = sub.UpdateProduct(1, productParameterModel);

            expect.Should().BeTrue();
        }

        [Test]
        public void UpdateProduct_ShouldBe_False()
        {
            //arrange
            var fixture = new Fixture();
            var productParameterModel = fixture.Build<ProductInputModelForService>()
                .Create();
            var productConditionModel = this._mapper.Map<ProductInputModelForRepo>(productParameterModel);

            _productRepository.UpdateProduct(1, productConditionModel).ReturnsForAnyArgs(false);

            var sub = new ProductService(_productRepository, _mapper);

            var expect = sub.UpdateProduct(1, productParameterModel);

            expect.Should().BeFalse();
        }

        [Test]
        public void DeleteProduct_ShouldBe_True()
        {
            _productRepository.DeleteProduct(1).ReturnsForAnyArgs(true);

            var sub = new ProductService(this._productRepository, _mapper);

            var expect = sub.DeleteProduct(1);

            expect.Should().BeTrue();
        }

        [Test]
        public void DeleteProduct_ShouldBe_False()
        {
            _productRepository.DeleteProduct(1).ReturnsForAnyArgs(false);

            var sub = new ProductService(this._productRepository, _mapper);

            var expect = sub.DeleteProduct(1);

            expect.Should().BeFalse();
        }
    }
}