// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.Hosting;
using Pricewatch.Scraper.Abstract.Services;

internal class Worker : IHostedService
{
	private readonly IProductScraperScheduler _productScraperScheduler;

	public Worker(IProductScraperScheduler productScraperScheduler)
	{
		_productScraperScheduler = productScraperScheduler;
	}

	public async Task StartAsync(CancellationToken cancellationToken)
    {
		await _productScraperScheduler.StartAsync();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}