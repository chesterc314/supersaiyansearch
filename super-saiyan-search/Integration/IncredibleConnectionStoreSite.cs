using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using ScrapySharp.Extensions;
using SuperSaiyanSearch.Domain;
using SuperSaiyanSearch.Domain.Interfaces;
using SuperSaiyanSearch.Integration.Interfaces;

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
            var elements = doc.DocumentNode.CssSelect(".products-grid > li.item");
            var resultProducts = new List<Product>();
            foreach (var element in elements)
            {
                var productLinkElementAttributes = element.CssSelect(".grid-product-image > .product-image").First().Attributes;
                var sourceUrl = productLinkElementAttributes.AttributesWithName("href").First().Value;
                var name = productLinkElementAttributes.AttributesWithName("title").First().Value;
                var imageElementAttributes = element.CssSelect(".grid-product-image > .product-image > .grid-image-wrapper > img").First().Attributes;
                var imageUrl = imageElementAttributes.AttributesWithName("data-src").First().Value;
                var brand = name.Split(" ")[0];
                var cultures = new CultureInfo("en-US");
                var priceValue = element.CssSelect(".grid-product-price > .price").First().InnerText.Trim('\n').Trim('R');
                var price = Convert.ToDecimal(priceValue, cultures);

                resultProducts.Add( new Product{
                    Name = name,
                    Description = name,
                    Price = price,
                    Units = 1,
                    Brand = brand,
                    Source = "IncredibleConnection",
                    SourceUrl = sourceUrl,
                    ImageUrl =  imageUrl
                });
            }

            return resultProducts;
        }
    }
}