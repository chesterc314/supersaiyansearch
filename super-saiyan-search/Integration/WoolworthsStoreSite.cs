using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using SuperSaiyanSearch.Domain;
using SuperSaiyanSearch.Domain.Interfaces;
using SuperSaiyanSearch.Integration.Interfaces;

namespace SuperSaiyanSearch.Integration
{
    public class WoolworthsStoreSite : IStoreSite
    {
        private readonly IHttpClient _httpClient;

        public WoolworthsStoreSite(IHttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public IEnumerable<IProduct> Search(string keyword)
        {
            var headers = new List<KeyValuePair<string, string>>();
            var host = "www.woolworths.co.za";
            headers.Add(KeyValuePair.Create("Referer", $"https://{host}/cat?Ntt={keyword}&Dy=1"));
            headers.Add(KeyValuePair.Create("Host", host));
            var response = _httpClient.Get($"https://{host}/server/searchCategory?pageURL=%2Fcat&Ntt={keyword}&Dy=1", headers);
            var parent = JObject.Parse(response.Content);
            var contents = parent.Value<JArray>("contents");
            var mainContent = contents[0].Value<JArray>("mainContent");
            var contents2 = mainContent[0].Value<JArray>("contents");
            var records = contents2[0].Value<JArray>("records");

            var resultProducts = new List<Product>();
            if (records.Count > 0)
            {
                foreach (var result in records)
                {
                    var imageUrl = result.SelectToken("attributes.p_imageReference").Value<string>();
                    var sourceUrl = result.SelectToken("attributes.detailPageURL").Value<string>();
                    var name = result.SelectToken("attributes.p_displayName").Value<string>();
                    var brands = result.SelectToken("attributes.Brands");
                    var brand = brands != null ? brands.Value<string>() : null;
                    var price = result.SelectToken("startingPrice.p_pl10").Value<decimal>();

                    resultProducts.Add(new Product
                    {
                        Name = name,
                        Description = name,
                        Price = price,
                        Brand = brand,
                        Source = StoreSiteName.Woolworths.ToString(),
                        SourceUrl = $"https://{host}{sourceUrl}",
                        ImageUrl = $"https://{host}{imageUrl}",
                        Units = 1
                    });
                }
            }

            return resultProducts;
        }
    }
}