using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pricewatch.Scraper.Abstract.Options
{
	public class CountryScraperOptions
	{
		public required Dictionary<string, ProductScraperServiceOptions> ProductScraperServiceOptions { get; set; }
	}
}
