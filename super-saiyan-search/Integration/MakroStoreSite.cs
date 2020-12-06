using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using ScrapySharp.Extensions;
using SuperSaiyanSearch.Domain;
using SuperSaiyanSearch.Domain.Interfaces;
using SuperSaiyanSearch.Integration.Interfaces;
using System.Web;
using System.Threading.Tasks;

namespace SuperSaiyanSearch.Integration
{
    public class MakroStoreSite : IStoreSite
    {
        private readonly IWebScrapper _webScrapper;

        public MakroStoreSite(IWebScrapper webScrapper)
        {
            _webScrapper = webScrapper;
        }

        public IEnumerable<IProduct> Search(string keyword)
        {
            var url = "https://www.makro.co.za";
            var doc = _webScrapper.Scrap($"{url}/search?text={keyword}");
            var elements = doc.DocumentNode.CssSelect(".mak-product-tiles-container__product-tile");
            var resultProducts = new List<Product>();
            if (elements.Any())
            {
                var index = 0;
                foreach (var element in elements)
                {
                    var productLinkElementAttributes = element.CssSelect(".product-tile-inner > .product-tile-inner__img").First().Attributes;
                    var sourceUrl = $"{url}{productLinkElementAttributes.AttributesWithName("href").First().Value}";
                    var name = productLinkElementAttributes.AttributesWithName("title").First().Value;
                    var imageElementAttributes = element.CssSelect(".product-tile-inner > .product-tile-inner__img > img").First().Attributes;
                    var imageUrl = imageElementAttributes.AttributesWithName("data-src").First().Value;
                    var cultures = new CultureInfo("en-US");
                    var priceValue = element.CssSelect(".product-tile-inner > .price")
                    .First()
                    .InnerHtml
                    .Trim()
                    .Trim('\n')
                    .Trim('R')
                    .TrimStart()
                    .Replace("<span class=\"mak-product__cents\">", ".").Replace("</span>", "");
                    var price = Convert.ToDecimal(priceValue, cultures);

                    resultProducts.Add(new Product
                    {
                        Name = HttpUtility.HtmlDecode(name),
                        Description = HttpUtility.HtmlDecode(name),
                        Price = price,
                        Units = 1,
                        Brand = null,
                        Source = StoreSiteName.Makro.ToString(),
                        SourceUrl = sourceUrl,
                        ImageUrl = imageUrl,
                        Order = index
                    });
                    ++index;
                }
            }

            return resultProducts;
        }
    }
}