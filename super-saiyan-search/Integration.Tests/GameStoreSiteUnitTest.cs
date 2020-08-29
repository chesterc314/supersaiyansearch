using System.Linq;
using SuperSaiyanSearch.Domain.Interfaces;
using Xunit;

namespace SuperSaiyanSearch.Integration.Tests
{
    public class GameStoreSiteUnitTest
    {
        private readonly IStoreSite _gameStoreSite;

        public GameStoreSiteUnitTest()
        {
           _gameStoreSite = new GameStoreSite(new WebScrapper());
        }
        [Fact]
        public void GivenUrlWhenSearchingScrapThenReturnSiteContent()
        {
            var results = _gameStoreSite.Search("laptop");
            Assert.Equal(true, results.All(r => r.Price > 0));
            Assert.NotNull(results);
        }
    }
}