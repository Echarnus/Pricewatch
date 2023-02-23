using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pricewatch.Scraper.Abstract.Services;

public interface IProductScraperScheduler
{
	Task StartAsync();
}
