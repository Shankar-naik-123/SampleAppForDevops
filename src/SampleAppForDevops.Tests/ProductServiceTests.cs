using SampleAppForDevops.Models;
using SampleAppForDevops.Services;

namespace SampleAppForDevops.Tests
{
    public class ProductServiceTests
    {
        private readonly ProductService _productService;

        public ProductServiceTests()
        {
            _productService = new ProductService();
        }

        [Fact]
        public async Task Create_Should_Add_Product_And_Assign_Id()
        {
            // Arrange
            var product = new Product
            {
                Name = "Laptop",
                Price = 50000
            };

            // Act
            var result = await _productService.Create(product);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Laptop", result.Name);
            Assert.Equal(50000, result.Price);
        }

        [Fact]
        public async Task GetAll_Should_Return_All_Products()
        {
            // Arrange
            await _productService.Create(new Product
            {
                Name = "Mobile",
                Price = 20000
            });

            await _productService.Create(new Product
            {
                Name = "Tablet",
                Price = 30000
            });

            // Act
            var result = await _productService.GetAll();

            // Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetById_Should_Return_Product_When_Product_Exists()
        {
            // Arrange
            var createdProduct = await _productService.Create(new Product
            {
                Name = "Keyboard",
                Price = 1500
            });

            // Act
            var result = await _productService.GetById(createdProduct.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(createdProduct.Id, result!.Id);
            Assert.Equal("Keyboard", result.Name);
        }

        [Fact]
        public async Task GetById_Should_Return_Null_When_Product_Does_Not_Exist()
        {
            // Act
            var result = await _productService.GetById(999);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Update_Should_Return_True_And_Update_Product_When_Product_Exists()
        {
            // Arrange
            var createdProduct = await _productService.Create(new Product
            {
                Name = "Mouse",
                Price = 1000
            });

            var updatedProduct = new Product
            {
                Name = "Gaming Mouse",
                Price = 2500
            };

            // Act
            var result = await _productService.Update(createdProduct.Id, updatedProduct);

            var productAfterUpdate = await _productService.GetById(createdProduct.Id);

            // Assert
            Assert.True(result);
            Assert.NotNull(productAfterUpdate);
            Assert.Equal("Gaming Mouse", productAfterUpdate!.Name);
            Assert.Equal(2500, productAfterUpdate.Price);
        }

        [Fact]
        public async Task Update_Should_Return_False_When_Product_Does_Not_Exist()
        {
            // Arrange
            var updatedProduct = new Product
            {
                Name = "Monitor",
                Price = 10000
            };

            // Act
            var result = await _productService.Update(999, updatedProduct);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task Delete_Should_Return_True_When_Product_Exists()
        {
            // Arrange
            var createdProduct = await _productService.Create(new Product
            {
                Name = "Speaker",
                Price = 4000
            });

            // Act
            var result = await _productService.Delete(createdProduct.Id);

            var deletedProduct = await _productService.GetById(createdProduct.Id);

            // Assert
            Assert.True(result);
            Assert.Null(deletedProduct);
        }

        [Fact]
        public async Task Delete_Should_Return_False_When_Product_Does_Not_Exist()
        {
            // Act
            var result = await _productService.Delete(999);

            // Assert
            Assert.False(result);
        }
    }
}
