using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pricewatch.Scraper.Abstract.Options;

public class ProductScraperServiceOptions
{
	public required TimeSpan ScrapeSchedule { get; set; }
}
