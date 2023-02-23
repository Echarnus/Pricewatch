using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pricewatch.Scraper.Abstract.Services
{
	public interface IProductService
	{
		/// <summary>
		/// Fetches and publishes products
		/// </summary>
		/// <param name="country"></param>
		/// <param name="store"></param>
		/// <returns></returns>
		Task RunAsync(string country, string store);
	}
}
