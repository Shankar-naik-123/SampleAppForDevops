using Microsoft.AspNetCore.Mvc;
using Moq;
using SampleAppForDevops.Controllers;
using SampleAppForDevops.Models;
using SampleAppForDevops.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleAppForDevops.Tests
{
	public class ProductControllerTests
	{
		private readonly Mock<IProductService> _mockService;
		private readonly ProductController _controller;

		public ProductControllerTests()
		{
			_mockService = new Mock<IProductService>();
			_controller = new ProductController(_mockService.Object);
		}

		[Fact]
		public async Task GetAll_ReturnsOk()
		{
			var list = new List<Product> { new() { Id = 1, Name = "Test", Price = 50 } };
			_mockService.Setup(s => s.GetAll()).ReturnsAsync(list);

			var result = await _controller.GetAll();

			var okResult = Assert.IsType<OkObjectResult>(result);
			Assert.Equal(list, okResult.Value);
		}

		[Fact]
		public async Task GetById_ReturnsProduct_WhenFound()
		{
			var prod = new Product { Id = 1, Name = "Test", Price = 50 };
			_mockService.Setup(s => s.GetById(1)).ReturnsAsync(prod);

			var result = await _controller.GetById(1);

			var ok = Assert.IsType<OkObjectResult>(result);
			Assert.Equal(prod, ok.Value);
		}

		[Fact]
		public async Task GetById_ReturnsNotFound_WhenMissing()
		{
			_mockService.Setup(s => s.GetById(1)).ReturnsAsync((Product?)null);

			var result = await _controller.GetById(1);

			Assert.IsType<NotFoundResult>(result);
		}

		[Fact]
		public async Task Create_ReturnsCreated()
		{
			var newProd = new Product { Id = 1, Name = "Created", Price = 100 };
			_mockService.Setup(s => s.Create(newProd)).ReturnsAsync(newProd);

			var result = await _controller.Create(newProd);

			var created = Assert.IsType<CreatedAtActionResult>(result);
			Assert.Equal(newProd, created.Value);
		}

		[Fact]
		public async Task Update_ReturnsNoContent_WhenSuccess()
		{
			_mockService.Setup(s => s.Update(1, It.IsAny<Product>())).ReturnsAsync(true);

			var result = await _controller.Update(1, new Product());

			Assert.IsType<NoContentResult>(result);
		}

		[Fact]
		public async Task Delete_ReturnsNoContent_WhenSuccess()
		{
			_mockService.Setup(s => s.Delete(1)).ReturnsAsync(true);

			var result = await _controller.Delete(1);

			Assert.IsType<NoContentResult>(result);
		}

		[Fact]
		public async Task SearchByName_ReturnsMatchingProducts()
		{
			var products = new List<Product>
		{
			new() { Id = 1, Name = "Laptop", Price = 1000 },
			new() { Id = 2, Name = "Keyboard", Price = 50 }
		};

			_mockService.Setup(s => s.GetAll()).ReturnsAsync(products);

			var result = await _controller.SearchByName("laptop");

			var ok = Assert.IsType<OkObjectResult>(result);
			var list = Assert.IsType<List<Product>>(ok.Value);
			Assert.Single(list);
			Assert.Equal("Laptop", list[0].Name);
		}
	}
}
