using System.Linq;
using SuperSaiyanSearch.Domain.Interfaces;
using Xunit;

namespace SuperSaiyanSearch.Integration.Tests
{
    public class TakealotStoreSiteUnitTest
    {
        private readonly IStoreSite _takealotStoreSite;

        public TakealotStoreSiteUnitTest()
        {
           _takealotStoreSite = new TakealotStoreSite(new HttpClient());
        }
        [Fact]
        public void GivenUrlWhenSearchingScrapThenReturnSiteContent()
        {
            var results = _takealotStoreSite.Search("laptop");
            var hasPositivePrices = true;
            Assert.NotNull(results);
            Assert.Equal(hasPositivePrices, results.All(r => r.Price > 0));
        }
    }
}