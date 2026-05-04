using Microsoft.AspNetCore.Mvc;
using SampleAppForDevops.Models;
using SampleAppForDevops.Services.Abstractions;

namespace SampleAppForDevops.Controllers
{

	[ApiController]
	[Route("api/[controller]")]
	public class ProductController : ControllerBase
	{
		private readonly IProductService _service;

		public ProductController(IProductService service)
		{
			_service = service;
		}

		// GET: api/product
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var products = await _service.GetAll();
			return Ok(products);
		}

		// GET: api/product/5
		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var product = await _service.GetById(id);
			return product == null ? NotFound() : Ok(product);
		}

		// POST: api/product
		[HttpPost]
		public async Task<IActionResult> Create(Product product)
		{
			var created = await _service.Create(product);
			return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
		}

		// PUT: api/product/5
		[HttpPut("{id}")]
		public async Task<IActionResult> Update(int id, Product product)
		{
			var success = await _service.Update(id, product);
			return success ? NoContent() : NotFound();
		}

		// DELETE: api/product/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var success = await _service.Delete(id);
			return success ? NoContent() : NotFound();
		}

		// GET: api/product/search/laptop
		[HttpGet("search/{name}")]
		public async Task<IActionResult> SearchByName(string name)
		{
			var products = await _service.GetAll();
			var matches = products
				.Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
				.ToList();
			return Ok(matches);
		}
		[HttpGet("dummy")]
		public async Task<IActionResult> DummyAPI()
		{
			
			return Ok("I am a dummy api version 4");
		}
	}
}
