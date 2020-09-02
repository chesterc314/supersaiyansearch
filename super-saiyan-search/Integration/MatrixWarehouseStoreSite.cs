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
    public class MatrixWarehouseStoreSite : IStoreSite
    {
        private IWebScrapper _webScrapper;
        public MatrixWarehouseStoreSite(IWebScrapper webScrapper)
        {
            _webScrapper = webScrapper;
        }
        public IEnumerable<IProduct> Search(string keyword)
        {
            var url = "https://matrixwarehouse.co.za";
            var doc = _webScrapper.Scrap($"{url}/?s={keyword}&product_cat=0&post_type=product");
            var elements = doc.DocumentNode.CssSelect(".product.type-product");
            var resultProducts = new List<Product>();
            if (elements.Any())
            {
                foreach (var element in elements)
                {
                    var productLinkElement = element.CssSelect(".product-inner > .woocommerce-LoopProduct-link.woocommerce-loop-product__link").FirstOrDefault();
                    if (productLinkElement != null)
                    {
                        var productLinkElementAttributes = productLinkElement.Attributes;
                        var sourceUrl = productLinkElementAttributes.AttributesWithName("href").First().Value;
                        var name = element.CssSelect(".product-inner > .woocommerce-LoopProduct-link.woocommerce-loop-product__link > .woocommerce-loop-product__title").First().InnerText;
                        var imageElementAttributes = element.CssSelect(".product-inner > .woocommerce-LoopProduct-link.woocommerce-loop-product__link > .attachment-woocommerce_thumbnail").First().Attributes;
                        var imageUrl = imageElementAttributes.AttributesWithName("src").First().Value;
                        var brand = name.Split(" ")[0];
                        var cultures = new CultureInfo("en-US");
                        var regularPriceBox = element.CssSelect(".product-inner > .woocommerce-LoopProduct-link.woocommerce-loop-product__link > .price > .woocommerce-Price-amount.amount").FirstOrDefault();
                        var specialPriceBox = element.CssSelect(".product-inner > .woocommerce-LoopProduct-link.woocommerce-loop-product__link > .price > ins > .woocommerce-Price-amount.amount");
                        var priceValue = (regularPriceBox ?? specialPriceBox.First()).InnerText.Replace("&#82;", "").Trim('\n').Trim('R').Trim();
                        var price = Convert.ToDecimal(priceValue, cultures);

                        resultProducts.Add(new Product
                        {
                            Name = HttpUtility.HtmlDecode(name),
                            Description = HttpUtility.HtmlDecode(name),
                            Price = price,
                            Units = 1,
                            Brand = brand,
                            Source = StoreSiteName.MatrixWarehouse.ToString(),
                            SourceUrl = sourceUrl,
                            ImageUrl = imageUrl
                        });
                    }
                }
            }
            return resultProducts;
        }
    }
}