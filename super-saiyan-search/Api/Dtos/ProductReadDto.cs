using Newtonsoft.Json;
using SuperSaiyanSearch.Domain.Interfaces;

namespace SuperSaiyanSearch.Api
{
    public class ProductReadDto : IProduct
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("brand")]
        public string Brand { get; set; }
        [JsonProperty("price")]
        public decimal Price { get; set; }
        [JsonProperty("units")]
        public int Units { get; set; }
        [JsonProperty("source")]
        public string Source { get; set; }
        [JsonProperty("sourceUrl")]
        public string SourceUrl { get; set; }
        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }
    }
}