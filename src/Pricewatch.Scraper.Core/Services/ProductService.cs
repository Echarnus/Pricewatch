using Autofac.Features.AttributeFilters;
using Autofac.Features.Indexed;
using Pricewatch.Scraper.Abstract.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pricewatch.Scraper.Core.Services
{
	internal class ProductService : IProductService
	{
		private readonly IProductPublisher _productPublisher;
		private readonly IIndex<string, IProductScraperService> _productScraperServiceIndex;

		public ProductService(
				IProductPublisher productPublisher,
				IIndex<string, IProductScraperService> productScraperServiceIndex
			)
		{
			_productPublisher = productPublisher;
			_productScraperServiceIndex = productScraperServiceIndex;
		}

		public async Task RunAsync(string countryCode, string store)
		{
			var productServiceScraper = _productScraperServiceIndex[$"{countryCode}-{store}"];
			await foreach(var category in productServiceScraper.GetCategoriesAsync())
			{
				await foreach (var product in productServiceScraper.ScrapeProductsAsync(category))
				{
					_productPublisher.Publish(new Abstract.Models.ProductEvent()
					{
						Product = product,
						RetrievalUtc = DateTime.UtcNow
					});
				}
			}
		}
	}
}
