using SuperSaiyanSearch.Domain.Interfaces;
using Xunit;

namespace SuperSaiyanSearch.Integration.Tests
{
    public class TakealotStoreSiteUnitTest
    {
        private readonly IStoreSite _takealotStoreSite;

        public TakealotStoreSiteUnitTest()
        {
           _takealotStoreSite = new TakealotStoreSite();
        }
        [Fact]
        public void GivenUrlWhenSearchingScrapThenReturnSiteContent()
        {
            var result = _takealotStoreSite.Search("laptop");
            Assert.NotNull(result);
        }
    }
}