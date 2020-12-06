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
    public class HifiCorpStoreSite : IStoreSite
    {
        private IWebScrapper _webScrapper;

        public HifiCorpStoreSite(IWebScrapper webScrapper)
        {
            this._webScrapper = webScrapper;
        }

        public IEnumerable<IProduct> Search(string keyword)
        {
            var url = "https://www.hificorp.co.za";
            var doc = _webScrapper.Scrap($"{url}/catalog/category/searchResult?q={keyword}");
            var elements = doc.DocumentNode.CssSelect(".products-grid > li.item");
            var resultProducts = new List<Product>();
            if (elements.Any())
            {
                foreach (var element in elements)
                {
                    var productLinkElementAttributes = element.CssSelect(".product-wrapper > .img-wrapper > .product-image").First().Attributes;
                    var sourceUrl = productLinkElementAttributes.AttributesWithName("href").First().Value;
                    var name = productLinkElementAttributes.AttributesWithName("title").First().Value;
                    var imageElementAttributes = element.CssSelect(".product-wrapper > .img-wrapper > .product-image > .grid-image-wrapper > img").First().Attributes;
                    var imageUrl = imageElementAttributes.AttributesWithName("data-src").First().Value;
                    var cultures = new CultureInfo("en-US");
                    var regularPriceBox = element.CssSelect(".product-wrapper > .price-grid-box > .regular-price-box > .regular-price").FirstOrDefault();
                    var specialPriceBox = element.CssSelect(".product-wrapper > .price-grid-box > .special-price-box > .regular-price");
                    var priceValue = (regularPriceBox ?? specialPriceBox.First()).InnerHtml.Trim('\n').Trim('R').Trim();
                    var price = Convert.ToDecimal(priceValue, cultures);

                    resultProducts.Add(new Product
                    {
                        Name = HttpUtility.HtmlDecode(name),
                        Description = HttpUtility.HtmlDecode(name),
                        Price = price,
                        Units = 1,
                        Brand = null,
                        Source = StoreSiteName.HifiCorp.ToString(),
                        SourceUrl = sourceUrl,
                        ImageUrl = imageUrl
                    });
                }
            }
            return Product.OrderedProducts(resultProducts);
        }
    }
}