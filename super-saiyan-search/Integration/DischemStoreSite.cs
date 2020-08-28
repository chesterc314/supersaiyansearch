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
    public class DischemStoreSite : IStoreSite
    {
        private IWebScrapper _webScrapper;

        public DischemStoreSite(IWebScrapper webScrapper)
        {
            _webScrapper = webScrapper;
        }

        public IEnumerable<IProduct> Search(string keyword)
        {
            var url = "https://www.dischem.co.za";
            var doc = _webScrapper.Scrap($"{url}/catalogsearch/result/?q={keyword}");
            var elements = doc.DocumentNode.CssSelect(".item.product.product-item");
            var resultProducts = new List<Product>();
            if (elements.Any())
            {
                foreach (var element in elements)
                {
                    var productLinkElementAttributes = element.CssSelect(".product-item-info > .image > .product-item-photo").First().Attributes;
                    var sourceUrl = productLinkElementAttributes.AttributesWithName("href").First().Value;
                    var name = element.CssSelect(".product-item-info > .product-item-details > .product-item-name > .product-item-link").First().InnerText;
                    var imageElementAttributes = element.CssSelect(".product-item-info > .image > .product-item-photo > .product-image-container > .product-image-wrapper > .product-image-photo").First().Attributes;
                    var imageUrl = $"{url}{imageElementAttributes.AttributesWithName("src").First().Value}";
                    var brand = name.Split(" ")[0];
                    var cultures = new CultureInfo("en-US");
                    var priceElement = element
                    .CssSelect(".product-item-info > .product-item-details > .price-box-wrapper-listing > .price-box > .price-box-inner-wrapper > .price-container > .price-wrapper");
                    var specialElement = element
                    .CssSelect(".product-item-info > .product-item-details > .price-box-wrapper-listing > .price-box > .price-box-inner-wrapper > .special-price > .price-container > .price-wrapper");
                    var priceValue = (priceElement.FirstOrDefault() ?? specialElement.First()).Attributes.AttributesWithName("data-price-amount").First().Value;
                    var price = Convert.ToDecimal(priceValue, cultures);

                    resultProducts.Add(new Product
                    {
                        Name = name,
                        Description = name,
                        Price = price,
                        Units = 1,
                        Brand = brand,
                        Source = "Dischem",
                        SourceUrl = sourceUrl,
                        ImageUrl = imageUrl
                    });
                }
            }
            return resultProducts;
        }
    }
}