using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pricewatch.Scraper.Abstract.Models;

public class Product
{
	public required string Brand { get; init; }
	public required string Name { get; init; }
	public string? Allergens { get; init; }
	public decimal Price { get; init; }
	public decimal PricePerUnit { get; init; }

}
