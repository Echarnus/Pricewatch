using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V108.Network;
using Pricewatch.Scraper.Abstract.Models;
using Pricewatch.Scraper.Abstract.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pricewatch.Scraper.Infrastructure.Services.Providers.Belgium;

public class ColruytProductScraperService : IProductScraperService
{
	private IWebDriver _webDriver = new ChromeDriver();

	public async IAsyncEnumerable<Product> ScrapeProductsAsync(ProductCategory productCategory)
	{
		void showAllItems()
		{
			
		}

		_webDriver.Navigate().GoToUrl(productCategory.Url);
		IWebElement? showMoreButton = null;
		do
		{
			try
			{
				showMoreButton = _webDriver.FindElement(By.XPath(@"//button[@id=""showMore""]"));
				showMoreButton.Click();
			}
			catch(Exception ex)
			{
				showMoreButton = null;
				Console.WriteLine("");
			}


		} while (showMoreButton != null);

		var productElements = _webDriver.FindElements(By.XPath(@"//div[contains(@class, ""product-item"")]"));
		foreach(var productElement in productElements)
		{
			string? productName = null, brand = null, allergens = null;
			decimal? basePrice = null, unitPrice = null;
			try
			{
				brand = productElement.GetAttribute("data-product-brand");
				productName = productElement.GetAttribute("data-tms-product-name");
				allergens = productElement.GetAttribute("data-product-allergens");

				var priceSegment = productElement.FindElement(By.ClassName("product__price__base-price"));
				basePrice = Decimal.Parse(priceSegment.GetAttribute("data-baseprice"));
				unitPrice = Decimal.Parse(priceSegment.GetAttribute("data-basepricevol"));

			} catch
			{
				Console.WriteLine("");
			}

			if(productName != null && brand != null && basePrice != null && unitPrice != null)
			{
				yield return new Product()
				{
					Brand = brand,
					Name = productName,
					Allergens = allergens,
					Price = basePrice.Value,
					PricePerUnit = unitPrice.Value
				};
			}
		}



		yield break;
	}

	public async IAsyncEnumerable<ProductCategory> GetCategoriesAsync()
	{
		_webDriver.Navigate().GoToUrl("https://www.collectandgo.be/colruyt/nl/");
		var categories = _webDriver.FindElements(By.XPath(@"//div[@class=""c-assortment__item""]"));
		foreach (var category in categories)
		{

			var categoryString = category.GetAttribute("innerHTML");
			string ? url = null, name = null;
			try
			{
				var ankerElement = category.FindElement(By.XPath(@"./child::a"));
				url = ankerElement.GetAttribute("href");
				name = ankerElement.GetAttribute("innerHTML").Trim();

			}
			catch
			{
				Console.WriteLine();
			}

			if (url != null && name != null)
			{
				yield return new ProductCategory()
				{
					Name = name,
					Store = new Store() { Name = "Colruyt" },
					Url = url
				};
			}
		}
		yield break;
	}
}
