using System.Linq;
using SuperSaiyanSearch.Domain.Interfaces;
using Xunit;

namespace SuperSaiyanSearch.Integration.Tests
{
    public class ShopriteStoreSiteUnitTest
    {
        private readonly IStoreSite _shopriteStoreSite;

        public ShopriteStoreSiteUnitTest()
        {
           _shopriteStoreSite = new ShopriteStoreSite(new WebScrapper());
        }
        [Fact]
        public void GivenUrlWhenSearchingScrapThenReturnSiteContent()
        {
            var results = _shopriteStoreSite.Search("bread");
            var hasPositivePrices = true;
            Assert.NotNull(results);
            Assert.Equal(hasPositivePrices, results.All(r => r.Price > 0));
        }
    }
}