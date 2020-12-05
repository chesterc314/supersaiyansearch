using System.Linq;
using SuperSaiyanSearch.Domain.Interfaces;
using Xunit;

namespace SuperSaiyanSearch.Integration.Tests
{
    public class WoolworthsStoreSiteUnitTest
    {
        private readonly IStoreSite _woolworthsStoreSite;

        public WoolworthsStoreSiteUnitTest()
        {
           _woolworthsStoreSite = new WoolworthsStoreSite(new HttpClient());
        }
        [Fact]
        public void GivenUrlWhenSearchingScrapThenReturnSiteContent()
        {
            var results = _woolworthsStoreSite.Search("bread");
            var hasPositivePrices = true;
            Assert.NotNull(results);
            Assert.Equal(hasPositivePrices, results.All(r => r.Price > 0));
        }
    }
}