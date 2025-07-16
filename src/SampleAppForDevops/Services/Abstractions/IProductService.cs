using SampleAppForDevops.Models;

namespace SampleAppForDevops.Services.Abstractions
{
	public interface IProductService
	{
		Task<List<Product>> GetAll();
		Task<Product?> GetById(int id);
		Task<Product> Create(Product product);
		Task<bool> Update(int id, Product updatedProduct);
		Task<bool> Delete(int id);
	}
}
