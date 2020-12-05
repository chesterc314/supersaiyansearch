using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using ScrapySharp.Extensions;
using SuperSaiyanSearch.Domain;
using SuperSaiyanSearch.Domain.Interfaces;
using SuperSaiyanSearch.Integration.Interfaces;
using System.Web;
using System.Threading.Tasks;

namespace SuperSaiyanSearch.Integration
{
    public class IncredibleConnectionStoreSite : IStoreSite
    {
        private readonly IWebScrapper _webScrapper;
        public IncredibleConnectionStoreSite(IWebScrapper webScrapper)
        {
            _webScrapper = webScrapper;
        }
        public IEnumerable<IProduct> Search(string keyword)
        {
            var url = "https://www.incredible.co.za";
            var doc = _webScrapper.Scrap($"{url}/catalogsearch/result/?q={keyword}");
            var elements = doc.DocumentNode.CssSelect("li.item.product.product-item");
            var resultProducts = new List<Product>();
            if (elements.Any())
            {
                Parallel.ForEach(elements, element =>
                {
                    var productLinkElementAttributes = element.CssSelect(".product-item-info .product").First().Attributes;
                    var sourceUrl = productLinkElementAttributes.AttributesWithName("href").First().Value;
                    var imageElementAttributes = element.CssSelect(".product-image-photo").First().Attributes;
                    var imageUrl = imageElementAttributes.AttributesWithName("src").First().Value;
                    var name = imageElementAttributes.AttributesWithName("alt").First().Value;
                    var cultures = new CultureInfo("en-US");
                    var priceValue = element.CssSelect(".product-item-details .price").First().InnerText.Replace("\n", "").Replace("R", "");
                    var price = Convert.ToDecimal(priceValue, cultures);

                    resultProducts.Add(new Product
                    {
                        Name = HttpUtility.HtmlDecode(name),
                        Description = name,
                        Price = price,
                        Units = 1,
                        Brand = null,
                        Source = StoreSiteName.IncredibleConnection.ToString(),
                        SourceUrl = sourceUrl,
                        ImageUrl = imageUrl
                    });
                });
            }

            return Product.OrderedProducts(resultProducts);
        }
    }
}