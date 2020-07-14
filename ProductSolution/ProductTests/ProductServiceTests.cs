using Domain.Enums;
using Domain.Interfaces;
using Domain.Models;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Services;
using Xunit;

namespace ProductTests
{
    public class ProductServiceTests
    {
        private readonly ProductService _systemUnderTest;
        private readonly Mock<IProductRepository> _productRepositoryMock = new Mock<IProductRepository>();        
        private List<Product> _products;

        public ProductServiceTests()
        {
            _systemUnderTest = new ProductService(_productRepositoryMock.Object);
            Setup();
        }


        [Fact]
        public async Task Read_ShouldReadAllProducts()
        {
            //Arange
            _productRepositoryMock.Setup(p => p.Read())
                .ReturnsAsync(_products);

            //Act
            var result = await _systemUnderTest.Read();

            //Asert
            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task Read_ShouldReadProductById()
        {
            //Arange
            _productRepositoryMock.Setup(p => p.Read("1"))
                .ReturnsAsync(_products.Where(p => p.Id == "1").FirstOrDefault());
            _productRepositoryMock.Setup(p => p.Read("2"))
                .ReturnsAsync(_products.Where(p => p.Id == "2").FirstOrDefault());

            //Act
            var result = await _systemUnderTest.Read("2");

            //Asert
            result.Description.Should().Be("Macbook Pro 15");
        }

        [Fact]
        public async Task Create_ShouldCreateProduct()
        {
            //Arange
            var product =  new Product
            {
                Id = "3",
                Description = "Cannon Color Max",
                Model = "Printer",
                Brand = "Cannon"
            };

            _productRepositoryMock.Setup(p => p.Create(It.IsAny<Product>()))
               .Callback((Product product) => _products.Add(product));

            //Act
            await _systemUnderTest.Create(product);

            //Asert
            var newProduct = _products.Where(p => p.Id == "3").FirstOrDefault();
            newProduct.Brand.Should().Be("Cannon");
        }

        [Fact]
        public async Task Update_ShouldUpdateProduct()
        {
            //Arange
            var product = new Product
            {
                Id = "2",
                Description = "Macbook Air",
                Model = "Laptop",
                Brand = "Apple"
            };

            _productRepositoryMock.Setup(p => p.Update(It.IsAny<Product>()))
               .Callback((Product product) =>
               {
                   var productToUpdate = _products.Where(p => p.Id == product.Id).FirstOrDefault();
                   productToUpdate.Description = product.Description;
                   productToUpdate.Model = product.Model;
                   productToUpdate.Brand = product.Brand;
               });

            //Act
            await _systemUnderTest.Update(product);

            //Asert
            var newProduct = _products.Where(p => p.Id == "2").FirstOrDefault();
            newProduct.Description.Should().Be("Macbook Air");
        }

        [Fact]
        public async Task Delete_ShouldDeleteProduct()
        {
            //Arange
            _productRepositoryMock.Setup(p => p.Delete(It.IsAny<string>()))
               .Callback((string id) =>
               {
                   var productToDelete = _products.Where(p => p.Id == id).FirstOrDefault();
                   _products.Remove(productToDelete);
               });

            //Act
            await _systemUnderTest.Delete("2");

            //Asert
            _products.Exists(p => p.Id == "2").Should().BeFalse();
        }

        [Fact]
        public void ProductExists_ShouldCheckIfExsists()
        {
            //Arange
            _productRepositoryMock.Setup(p => p.ProductExists("1"))
               .Returns(true);
            _productRepositoryMock.Setup(p => p.ProductExists("2"))
               .Returns(false);

            //Act
            var result = _systemUnderTest.ProductExists("2");

            //Asert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task FilterProducts_ShouldFilterByValue()
        {
            //Arange
            _productRepositoryMock.Setup(p => p.FilterProducts(Filter.Brand, "Dell"))
               .ReturnsAsync(_products.Where(p => p.Brand.Contains("Dell")));

            //Act
            var result = await _systemUnderTest.FilterProducts(Filter.Brand, "Dell");

            //Asert
            result.FirstOrDefault().Brand.Should().Be("Dell");
        }

        private void Setup()
        {
            _products = new List<Product>();

            _products.Add(new Product()
            {
                Id = "1",
                Description = "Dell G5 15",
                Model = "Laptop",
                Brand = "Dell"
            });

            _products.Add(new Product()
            {
                Id = "2",
                Description = "Macbook Pro 15",
                Model = "Laptop",
                Brand = "Apple"
            });
        }
    }
}
