using System.Linq;
using System.Collections.Generic;
using AutoMapper;
using Moq;
using SuperSaiyanSearch.Api.Interfaces;
using SuperSaiyanSearch.Integration;
using Xunit;
using SuperSaiyanSearch.Domain;

namespace SuperSaiyanSearch.Api.Tests
{
    public class ProductApiUnitTest
    {
        private readonly IProductApi _productApi;

        public ProductApiUnitTest()
        {
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<IEnumerable<ProductReadDto>>(It.IsAny<IEnumerable<Product>>()))
                .Returns((IEnumerable<Product> source) =>
                    {
                        return source.Select(p => new ProductReadDto
                        {
                            Name = p.Name,
                            Price = p.Price,
                            Source = p.Source,
                            SourceUrl = p.SourceUrl,
                            ImageUrl = p.ImageUrl
                        });
                    });
            _productApi = new ProductApi(new StoreSiteAggregation(new StoreSiteConfiguration(new WebScrapper(), new HttpClient())), mockMapper.Object);
        }
        [Fact]
        public void GivenKeywordWhenInvokingSearchThenReturnPostivePriceValues()
        {
            var keyword = "laptop";
            var isPositivePrice = true;
            var results = _productApi.Search(keyword);
            Assert.Equal(isPositivePrice, results.Products.All(p => p.Price > 0));
        }
    }
}
