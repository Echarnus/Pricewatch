using Confluent.Kafka;
using Microsoft.Extensions.Options;
using OpenQA.Selenium;
using Pricewatch.Scraper.Abstract.Models;
using Pricewatch.Scraper.Abstract.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pricewatch.Scraper.Infrastructure.Services.Publishers
{
	internal class ProductPublisher : IProductPublisher
	{
		private readonly IOptions<ProducerConfig> _producerConfig;

		public ProductPublisher(IOptions<ProducerConfig> producerConfig)
		{
			this._producerConfig = producerConfig;
		}

		public void Publish(ProductEvent productEvent)
		{
			using var producer = new ProducerBuilder<Null, ProductEvent>(_producerConfig.Value).Build();
			producer.Produce(nameof(ProductEvent), new Message<Null, ProductEvent> { Value = productEvent });
		}
	}
}
