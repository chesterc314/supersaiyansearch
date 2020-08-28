using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using ScrapySharp.Extensions;
using SuperSaiyanSearch.Domain;
using SuperSaiyanSearch.Domain.Interfaces;
using SuperSaiyanSearch.Integration.Interfaces;

namespace SuperSaiyanSearch.Integration
{
    public class ClicksStoreSite : IStoreSite
    {
        private IWebScrapper _webScrapper;

        public ClicksStoreSite(IWebScrapper webScrapper)
        {
            _webScrapper = webScrapper;
        }

        public IEnumerable<IProduct> Search(string keyword)
        {
            var url = "https://clicks.co.za";
            var doc = _webScrapper.Scrap($"{url}/search/?cat=&text={keyword}");
            var elements = doc.DocumentNode.CssSelect(".plpsep");
            var resultProducts = new List<Product>();
            if (elements.Any())
            {
                foreach (var element in elements)
                {
                    var productLinkElementAttributes = element.CssSelect(".productBlock > a").First().Attributes;
                    var sourceUrl = $"{url}{productLinkElementAttributes.AttributesWithName("href").First().Value}";
                    var name = productLinkElementAttributes.AttributesWithName("title").First().Value;
                    var imageElementAttributes = element.CssSelect(".productBlock > a > img").First().Attributes;
                    var imageUrl = $"{url}{imageElementAttributes.AttributesWithName("src").First().Value}";
                    var brand = element.CssSelect(".productBlock > .detailContent > a > h5").First().Attributes.AttributesWithName("title").First().Value;
                    var cultures = new CultureInfo("en-US");
                    var priceValue = element.CssSelect(".productBlock > .detailContent > .price-wrap > .price")
                    .First().InnerText.Replace("R", "").Trim();
                    var price = Convert.ToDecimal(priceValue, cultures);

                    resultProducts.Add(new Product
                    {
                        Name = name,
                        Description = name,
                        Price = price,
                        Units = 1,
                        Brand = brand,
                        Source = StoreSiteName.Clicks.ToString(),
                        SourceUrl = sourceUrl,
                        ImageUrl = imageUrl
                    });
                }
            }
            return resultProducts;
        }
    }
}