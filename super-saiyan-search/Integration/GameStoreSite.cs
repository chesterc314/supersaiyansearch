using System.Collections.Generic;
using SuperSaiyanSearch.Domain;
using SuperSaiyanSearch.Domain.Interfaces;
using System.Linq;
using System;
using System.Globalization;
using SuperSaiyanSearch.Integration.Interfaces;
using ScrapySharp.Extensions;

namespace SuperSaiyanSearch.Integration
{
    public class GameStoreSite : IStoreSite
    {
        private readonly IWebScrapper _webScrapper;
        public GameStoreSite(IWebScrapper webScrapper)
        {
            _webScrapper = webScrapper;
        }
        public IEnumerable<IProduct> Search(string keyword)
        {
            var url = "https://www.game.co.za";
            var doc = _webScrapper.Scrap($"{url}/game-za/en/search/?text={keyword}");
            var elements = doc.DocumentNode.CssSelect(".product-item.productListerGridDiv");
            var resultProducts = new List<Product>();
            foreach (var element in elements)
            {
                var productLinkElementAttributes = element.CssSelect(".thumb.gtmProductLink").First().Attributes;
                var sourceUrl = $"{url}{productLinkElementAttributes.AttributesWithName("href").First().Value}";
                var name = productLinkElementAttributes.AttributesWithName("title").First().Value;
                var imageElementAttributes = element.CssSelect(".productPrimaryImage > img").First().Attributes;
                var imageUrl = $"{url}{imageElementAttributes.AttributesWithName("src").First().Value}";
                var brand = element.CssSelect(".details > .product-brand > .brand.gtmProductLink").First().InnerText.Trim('\n', '\t');
                var cultures = new CultureInfo("en-US");
                var price = Convert.ToDecimal(element.CssSelect(".details > .price > .finalPrice").First().InnerText.Trim('R'), cultures);

                resultProducts.Add( new Product{
                    Name = name,
                    Description = name,
                    Price = price,
                    Units = 1,
                    Brand = brand,
                    Source = "Game",
                    SourceUrl = sourceUrl,
                    ImageUrl =  imageUrl
                });
            }

            return resultProducts;
        }
    }
}