using System.Collections.Generic;
using System.Linq;
using SuperSaiyanSearch.Domain;
using SuperSaiyanSearch.Domain.Interfaces;
using SuperSaiyanSearch.Integration;
using SuperSaiyanSearch.Integration.Interfaces;
using Xunit;

namespace SuperSaiyanSearch.Integration.Tests
{
    public class StoreSiteAggregationUnitTest
    {
        private class MockStoreSite : IStoreSite
        {
            public IEnumerable<IProduct> Search(string keyword)
            {
                return new List<IProduct> {
                    new Product(){
                        Name = "vga to hdmi"
                    },
                    new Product(){
                        Name = "ball"
                    },
                    new Product(){
                        Name = "tennis racket"
                    }
                }.Where(product => product.Name.Contains(keyword.ToLower()));
            }
        }
        private class MockStoreSiteConfiguration : IStoreSiteConfiguration
        {
            public IEnumerable<IStoreSite> StoreSites
            {
                get
                {
                    return new List<IStoreSite>(){
                        new MockStoreSite()
                    };
                }
            }
        }
        private readonly IStoreSiteAggregation _storeSiteAggregation;
        public StoreSiteAggregationUnitTest()
        {
            _storeSiteAggregation = new StoreSiteAggregation(new MockStoreSiteConfiguration());
        }
        [Fact]
        public void GivenStoreSitesWhenSearchThenReturnProducts()
        {
            var name = "vga to hdmi";
            var products = _storeSiteAggregation.SearchAll("hdmi").ToList();
            var product = products.First();
            Assert.Equal(product.Name, name);
        }
    }
}
