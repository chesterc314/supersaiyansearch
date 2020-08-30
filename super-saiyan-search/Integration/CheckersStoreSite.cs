using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using ScrapySharp.Extensions;
using SuperSaiyanSearch.Domain;
using SuperSaiyanSearch.Domain.Interfaces;
using SuperSaiyanSearch.Integration.Interfaces;
using System.Web;

namespace SuperSaiyanSearch.Integration
{
    public class CheckersStoreSite : IStoreSite
    {
        private IWebScrapper _webScrapper;

        public CheckersStoreSite(IWebScrapper webScrapper)
        {
            this._webScrapper = webScrapper;
        }

        public IEnumerable<IProduct> Search(string keyword)
        {
            var url = "https://www.checkers.co.za";
            var doc = _webScrapper.Scrap($"{url}/search?q={keyword}");
            var elements = doc.DocumentNode.CssSelect(".product-frame");
            var resultProducts = new List<Product>();
            if (elements.Any())
            {
                foreach (var element in elements)
                {
                    var productLinkElementAttributes = element.CssSelect(".item-product > .item-product__content > .item-product__image > .product-listening-click").First().Attributes;
                    var sourceUrl = $"{url}{productLinkElementAttributes.AttributesWithName("href").First().Value}";
                    var name = productLinkElementAttributes.AttributesWithName("title").First().Value;
                    var imageElementAttributes = element.CssSelect(".item-product > .item-product__content > .item-product__image > .product-listening-click > img").First().Attributes;
                    var imageUrl = $"{url}{imageElementAttributes.AttributesWithName("data-src").First().Value}";
                    var brand = name.Split(" ")[0];
                    var cultures = new CultureInfo("en-US");
                    var priceValue = element.CssSelect(".item-product > .item-product__content > .item-product__caption > .item-product__details > .js-item-product-price > .special-price > .special-price__price > .now")
                    .First().InnerText.Replace("R", "").Trim();
                    var price = Convert.ToDecimal(priceValue, cultures);

                    resultProducts.Add(new Product
                    {
                        Name = HttpUtility.HtmlDecode(name),
                        Description = HttpUtility.HtmlDecode(name),
                        Price = price,
                        Units = 1,
                        Brand = brand,
                        Source = StoreSiteName.Checkers.ToString(),
                        SourceUrl = sourceUrl,
                        ImageUrl = imageUrl
                    });
                }
            }
            return resultProducts;
        }
    }
}