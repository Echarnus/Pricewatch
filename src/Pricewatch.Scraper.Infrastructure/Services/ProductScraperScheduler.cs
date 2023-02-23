using Autofac;
using Hangfire;
using Microsoft.Extensions.Options;
using Pricewatch.Scraper.Abstract.Options;
using Pricewatch.Scraper.Abstract.Services;
using Pricewatch.Scraper.Infrastructure.Services.Tasks;
using System.Runtime.CompilerServices;

namespace Pricewatch.Scraper.Infrastructure.Services;

internal class ProductScraperScheduler : IProductScraperScheduler, IDisposable
{
	private readonly BackgroundJobServer _jobServer;
	private readonly IOptionsMonitor<ScraperOptions> _scraperOptionsMonitor;
	private readonly IProductService _productService;
	private readonly ITaskCreator _taskCreator;
	private bool disposedValue;

	public ProductScraperScheduler(
		IOptionsMonitor<ScraperOptions> scraperOptionsMonitor, 
		IProductService productService,
		ITaskCreator taskCreator
	)
	{
		_scraperOptionsMonitor = scraperOptionsMonitor;
		_productService = productService;
		_taskCreator = taskCreator;
		_scraperOptionsMonitor.OnChange(OnScraperOptionsChanged);
	}

	private void OnScraperOptionsChanged(ScraperOptions scraperOptions, string? listener)
	{
		throw new NotImplementedException();
	}

	public Task StartAsync()
	{
		var options = _scraperOptionsMonitor.CurrentValue;
		
		foreach(var country in options.CountryScraperOptions)
		{
			foreach(var store in country.Value.ProductScraperServiceOptions)
			{
				_taskCreator.AddOrUpdateRecurringTask(() => _productService.RunAsync(country.Key, store.Key), store.Value.ScrapeSchedule);
			}
		}

		return Task.CompletedTask;
	}

	protected virtual void Dispose(bool disposing)
	{
		if (!disposedValue)
		{
			if (disposing)
			{
			}

			disposedValue = true;
		}
	}

	public void Dispose()
	{
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}

}
