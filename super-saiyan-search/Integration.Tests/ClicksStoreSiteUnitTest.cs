using System.Linq;
using SuperSaiyanSearch.Domain.Interfaces;
using Xunit;

namespace SuperSaiyanSearch.Integration.Tests
{
    public class ClicksStoreSiteUnitTest
    {
        private readonly IStoreSite _clicksStoreSite;

        public ClicksStoreSiteUnitTest()
        {
           _clicksStoreSite = new ClicksStoreSite(new WebScrapper());
        }
        [Fact]
        public void GivenUrlWhenSearchingScrapThenReturnSiteContent()
        {
            var results = _clicksStoreSite.Search("bread");
            Assert.Equal(true, results.All(r => r.Price > 0));
            Assert.NotNull(results);
        }
    }
}