using System.Linq;
using SuperSaiyanSearch.Domain.Interfaces;
using Xunit;

namespace SuperSaiyanSearch.Integration.Tests
{
    public class CheckersStoreSiteUnitTest
    {
        private readonly IStoreSite _checkersStoreSite;

        public CheckersStoreSiteUnitTest()
        {
           _checkersStoreSite = new CheckersStoreSite(new WebScrapper());
        }
        [Fact]
        public void GivenUrlWhenSearchingScrapThenReturnSiteContent()
        {
            var results = _checkersStoreSite.Search("bread");
            var hasPositivePrices = true;
            Assert.NotNull(results);
            Assert.Equal(hasPositivePrices, results.All(r => r.Price > 0));
        }
    }
}