using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pricewatch.Scraper.Abstract.Options;

public class ScraperOptions
{
	public required Dictionary<string, CountryScraperOptions> CountryScraperOptions { get; set; }
}
