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
            Assert.NotNull(results);
        }
    }
}