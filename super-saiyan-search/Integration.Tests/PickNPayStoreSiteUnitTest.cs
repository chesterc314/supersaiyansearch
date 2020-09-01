using System.Linq;
using SuperSaiyanSearch.Domain.Interfaces;
using Xunit;

namespace SuperSaiyanSearch.Integration.Tests
{
    public class PicknPayStoreSiteUnitTest
    {
        private readonly IStoreSite _pickNPayStoreSite;

        public PicknPayStoreSiteUnitTest()
        {
           _pickNPayStoreSite = new PicknPayStoreSite(new WebScrapper());
        }
        [Fact]
        public void GivenUrlWhenSearchingScrapThenReturnSiteContent()
        {
            var results = _pickNPayStoreSite.Search("bread");
            var hasPositivePrices = true;
            Assert.NotNull(results);
            Assert.Equal(hasPositivePrices, results.All(r => r.Price > 0));
        }
    }
}