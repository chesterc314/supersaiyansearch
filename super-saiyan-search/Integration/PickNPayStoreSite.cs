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
    public class PicknPayStoreSite : IStoreSite
    {
        private IWebScrapper _webScrapper;

        public PicknPayStoreSite(IWebScrapper webScrapper)
        {
            _webScrapper = webScrapper;
        }

        public IEnumerable<IProduct> Search(string keyword)
        {
            var url = "https://www.pnp.co.za";
            var doc = _webScrapper.Scrap($"{url}/pnpstorefront/pnp/en/search/?text={keyword}");
            var elements = doc.DocumentNode.CssSelect(".productCarouselItemContainer");
            var resultProducts = new List<Product>();
            if (elements.Any())
            {
                foreach (var element in elements)
                {
                    var productLinkElementAttributes = element.CssSelect(".productCarouselItem > .product-card-grid > a").First().Attributes;
                    var sourceUrl = $"{url}{productLinkElementAttributes.AttributesWithName("href").First().Value}";
                    var imageElementAttributes = element.CssSelect(".productCarouselItem > .product-card-grid > a > .thumb > img").First().Attributes;
                    var imageUrl = imageElementAttributes.AttributesWithName("src").First().Value;
                    var name = imageElementAttributes.AttributesWithName("title").First().Value;
                    var cultures = new CultureInfo("en-US");
                    var priceValue = element.CssSelect(".productCarouselItem > .product-card-grid > a > .product-price > .item-price > .currentPrice")
                    .First().InnerHtml.Replace("R", "").Replace("<span>", ".").Replace("</span>", "").Trim();
                    var price = Convert.ToDecimal(priceValue, cultures);

                    resultProducts.Add(new Product
                    {
                        Name = HttpUtility.HtmlDecode(name),
                        Description = name,
                        Price = price,
                        Units = 1,
                        Brand = null,
                        Source = StoreSiteName.PicknPay.ToString(),
                        SourceUrl = sourceUrl,
                        ImageUrl = imageUrl
                    });
                }
            }
            return resultProducts;
        }
    }
}