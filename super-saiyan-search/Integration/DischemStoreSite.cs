using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using ScrapySharp.Extensions;
using SuperSaiyanSearch.Domain;
using SuperSaiyanSearch.Domain.Interfaces;
using SuperSaiyanSearch.Integration.Interfaces;
using System.Web;
using System.Threading.Tasks;

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
                Parallel.ForEach(elements, element =>
                {
                    var productLinkElementAttributes = element.CssSelect(".product-item-info > .image > .product-item-photo").First().Attributes;
                    var sourceUrl = productLinkElementAttributes.AttributesWithName("href").First().Value;
                    var name = element.CssSelect(".product-item-info > .product-item-details > .product-item-name > .product-item-link").First().InnerText;
                    var imageElementAttributes = element.CssSelect(".product-item-info > .image > .product-item-photo > .product-image-container > .product-image-wrapper > .product-image-photo").First().Attributes;
                    var imageUrl = imageElementAttributes.AttributesWithName("src").First().Value;
                    var cultures = new CultureInfo("en-US");
                    var priceElement = element
                    .CssSelect(".product-item-info > .product-item-details > .price-box-wrapper-listing > .price-box > .price-box-inner-wrapper > .price-container > .price-wrapper");
                    var specialElement = element
                    .CssSelect(".product-item-info > .product-item-details > .price-box-wrapper-listing > .price-box > .price-box-inner-wrapper > .special-price > .price-container > .price-wrapper");
                    var priceVal = (priceElement.FirstOrDefault() ?? specialElement.FirstOrDefault());
                    var priceValue = (priceVal != null) ? priceVal.Attributes.AttributesWithName("data-price-amount").First().Value : "0";
                    var price = Convert.ToDecimal(priceValue, cultures);

                    resultProducts.Add(new Product
                    {
                        Name = $"{HttpUtility.HtmlDecode(name)}{(priceVal != null ? "" : "Out of stock")}",
                        Description = name,
                        Price = price,
                        Units = 1,
                        Brand = null,
                        Source = StoreSiteName.Dischem.ToString(),
                        SourceUrl = sourceUrl,
                        ImageUrl = imageUrl
                    });
                });
            }
            return Product.OrderedProducts(resultProducts);
        }
    }
}