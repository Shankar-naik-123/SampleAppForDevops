using System.Diagnostics.CodeAnalysis;

namespace SampleAppForDevops.Models
{
	[ExcludeFromCodeCoverage]
	public class Product
	{
		public int Id { get; set; }
		public string Name { get; set; } = null!;
		public decimal Price { get; set; }
	}
}
