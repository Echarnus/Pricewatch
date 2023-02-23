using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pricewatch.Scraper.Abstract.Models;

public class ProductEvent
{
	public required DateTime RetrievalUtc { get; init; }
	public required Product Product { get; init; }
}
