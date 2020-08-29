using System.Linq;
using SuperSaiyanSearch.Domain.Interfaces;
using Xunit;

namespace SuperSaiyanSearch.Integration.Tests
{
    public class DischemStoreSiteUnitTest
    {
        private readonly IStoreSite _dischemStoreSite;

        public DischemStoreSiteUnitTest()
        {
           _dischemStoreSite = new DischemStoreSite(new WebScrapper());
        }
        [Fact]
        public void GivenUrlWhenSearchingScrapThenReturnSiteContent()
        {
            var results = _dischemStoreSite.Search("bread");
            Assert.Equal(true, results.All(r => r.Price > 0));
            Assert.NotNull(results);
        }
    }
}