using Newtonsoft.Json;

namespace SuperSaiyanSearch.Api
{
    public class ProductReadDto
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("price")]
        public decimal Price { get; set; }
        [JsonProperty("source")]
        public string Source { get; set; }
        [JsonProperty("sourceUrl")]
        public string SourceUrl { get; set; }
        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }
    }
}