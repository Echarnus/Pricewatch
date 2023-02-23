using Autofac;
using Autofac.Extensions.DependencyInjection;
using Hangfire;
using Hangfire.Storage.SQLite;
using Microsoft.Extensions.Hosting;
using Pricewatch.Scraper.Abstract.Services;
using Pricewatch.Scraper.Infrastructure.Services;
using Pricewatch.Scraper.Infrastructure.Services.Providers.Belgium;
using Pricewatch.Scraper.Infrastructure.Services.Publishers;
using Pricewatch.Scraper.Infrastructure.Services.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pricewatch.Scraper.Infrastructure.Extensions.DependencyInjection;

public static class AutoFacExtensions
{
    public static ContainerBuilder AddPriceWatchInfrastructure(this ContainerBuilder container)
    {
		container.RegisterType<HangfireTaskCreator>().As<ITaskCreator>();
		container.RegisterType<ProductPublisher>().As<IProductPublisher>();
		container.RegisterType<ProductScraperScheduler>().As<IProductScraperScheduler>();
		container.RegisterType<ColruytProductScraperService>().Keyed<IProductScraperService>("be-colruyt"); ;
		return container;
    }

	public static IHost UsePriceWatchInfrastructure(this IHost host)
	{
		GlobalConfiguration.Configuration.UseAutofacActivator(((AutofacServiceProvider)host.Services).LifetimeScope, false);
		GlobalConfiguration.Configuration.UseSQLiteStorage();
		return host;
	}
}
