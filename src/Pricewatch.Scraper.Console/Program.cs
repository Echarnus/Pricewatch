// See https://aka.ms/new-console-template for more information

using Confluent.Kafka;
using Pricewatch.Scraper.Abstract.Models;
using Pricewatch.Scraper.Infrastructure.Services.Providers.Belgium;


using var producer = new ProducerBuilder<Null, string>(new ProducerConfig() { BootstrapServers = "localhost:9092" }).Build();
await producer.ProduceAsync(nameof(ProductEvent), new Message<Null, string> { Value = "Test!"});