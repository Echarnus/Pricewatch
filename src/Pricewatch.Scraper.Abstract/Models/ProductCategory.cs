using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pricewatch.Scraper.Abstract.Models
{
	public class ProductCategory
	{
		public required string Name { get; init; }
		public required string Url { get; init; }
		public required Store Store { get; init; }
	}
}
