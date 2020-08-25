using System.Collections.Generic;
using Newtonsoft.Json;

namespace SuperSaiyanSearch.Api
{
    public class ProductsReadDto
    {
        [JsonProperty("products")]
        public IEnumerable<ProductReadDto> Products { get; set; }
        [JsonProperty("next")]
        public string Next { get; set; }
        [JsonProperty("previous")]
        public string Previous { get; set; }
        [JsonProperty("limit")]
        public int Limit { get; set; }
        [JsonProperty("totalResults")]
        public int TotalResults { get; set; }
        public ProductsReadDto(IEnumerable<ProductReadDto> products)
        {
            Products = products;
        }
    }
}