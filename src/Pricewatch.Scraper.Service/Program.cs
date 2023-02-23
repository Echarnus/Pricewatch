using Autofac;
using Autofac.Extensions.DependencyInjection;
using Confluent.Kafka;
using Hangfire;
using Hangfire.Storage.SQLite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Pricewatch.Scraper.Abstract.Options;
using Pricewatch.Scraper.Core.Extensions.DependencyInjection;
using Pricewatch.Scraper.Infrastructure.Extensions.DependencyInjection;

static void ConfigureServices(HostBuilderContext hostBuildercontext, IServiceCollection services)
{
    services.AddHostedService<Worker>();

	services.Configure<ProducerConfig>(hostBuildercontext.Configuration.GetSection("PublishOptions"));
	services.Configure<ScraperOptions>(hostBuildercontext.Configuration.GetSection(nameof(ScraperOptions)));
}


static void ConfigureContainer(ContainerBuilder containerBuilder)
{
    containerBuilder
        .AddPricewatScraper()
        .AddPriceWatchInfrastructure();
}

IHost host = Host.CreateDefaultBuilder(args)
    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
	.ConfigureAppConfiguration((configurationBuilder) =>
	{
		configurationBuilder
			.AddJsonFile("AppSettings.json", true, true)
			.AddEnvironmentVariables();
	})
    .ConfigureServices(ConfigureServices)
    .ConfigureContainer<ContainerBuilder>(ConfigureContainer)
    .Build();

host.UsePriceWatchInfrastructure();

await host.RunAsync();