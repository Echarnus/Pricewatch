using Autofac;
using Pricewatch.Scraper.Abstract.Services;
using Pricewatch.Scraper.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pricewatch.Scraper.Core.Extensions.DependencyInjection;

public static class AutoFacExtensions
{
    public static ContainerBuilder AddPricewatScraper(this ContainerBuilder container)
    {
		container.RegisterType<ProductService>().As<IProductService>();

        return container;
    }
}
