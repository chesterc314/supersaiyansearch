using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using SuperSaiyanSearch.Domain;
using SuperSaiyanSearch.Domain.Interfaces;
using SuperSaiyanSearch.Integration.Interfaces;
using System.Web;

namespace SuperSaiyanSearch.Integration
{
    public class TakealotStoreSite : IStoreSite
    {
        private readonly IHttpClient _httpClient;

        public TakealotStoreSite(IHttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public IEnumerable<IProduct> Search(string keyword)
        {
            var response = _httpClient.Get($"https://api.takealot.com/rest/v-1-9-1/searches/products,filters,facets,sort_options,breadcrumbs,slots_audience,context,seo?qsearch={keyword}", new List<KeyValuePair<string, string>>());
            var parent = JObject.Parse(response.Content);
            var sections = parent.Value<JObject>("sections");
            var products = sections.Value<JObject>("products");
            var results = products.Value<JArray>("results");

            var resultProducts = new List<Product>();
            if (results.Count > 0)
            {
                foreach (var result in results)
                {
                    var slug = result.SelectToken("product_views.core.slug").Value<string>();
                    var innerProducts = result.SelectToken("product_views.enhanced_ecommerce_click.ecommerce.click.products").Value<JArray>();
                    var images = result.SelectToken("product_views.gallery.images");
                    var imageUrl = images.First.Value<string>();
                    var id = innerProducts.First.SelectToken("id").Value<string>();
                    var name = innerProducts.First.SelectToken("name").Value<string>();
                    var brand = innerProducts.First.SelectToken("brand").Value<string>();
                    var price = innerProducts.First.SelectToken("price").Value<decimal>();
                    var quantity = innerProducts.First.SelectToken("quantity").Value<int>();
                    var imageUrlParts = imageUrl.Split("{size}");
                    resultProducts.Add(new Product
                    {
                        Name = name,
                        Description = name,
                        Price = price,
                        Brand = brand,
                        Source = StoreSiteName.Takealot.ToString(),
                        SourceUrl = $"https://www.takealot.com/{slug}/{id}",
                        ImageUrl = $"{imageUrlParts[0]}fb{imageUrlParts[1]}",
                        Units = quantity
                    });
                }
            }

            return resultProducts;
        }
    }
}