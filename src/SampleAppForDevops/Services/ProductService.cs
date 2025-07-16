using SampleAppForDevops.Models;
using SampleAppForDevops.Services.Abstractions;

namespace SampleAppForDevops.Services
{
	public class ProductService : IProductService
	{
		private readonly List<Product> _products = new();
		private int _nextId = 1;

		public Task<List<Product>> GetAll()
		{
			return Task.FromResult(_products.ToList());
		}

		public Task<Product?> GetById(int id)
		{
			var product = _products.FirstOrDefault(p => p.Id == id);
			return Task.FromResult(product);
		}

		public Task<Product> Create(Product product)
		{
			product.Id = _nextId++;
			_products.Add(product);
			return Task.FromResult(product);
		}

		public Task<bool> Update(int id, Product updatedProduct)
		{
			var existing = _products.FirstOrDefault(p => p.Id == id);
			if (existing == null)
				return Task.FromResult(false);

			existing.Name = updatedProduct.Name;
			existing.Price = updatedProduct.Price;
			return Task.FromResult(true);
		}

		public Task<bool> Delete(int id)
		{
			var product = _products.FirstOrDefault(p => p.Id == id);
			if (product == null)
				return Task.FromResult(false);

			_products.Remove(product);
			return Task.FromResult(true);
		}
	}
}
