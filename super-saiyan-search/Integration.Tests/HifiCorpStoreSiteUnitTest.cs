using SuperSaiyanSearch.Domain.Interfaces;
using Xunit;

namespace SuperSaiyanSearch.Integration.Tests
{
    public class HifiCorpStoreSiteUnitTest
    {
        private readonly IStoreSite _hifiCorpStoreSite;

        public HifiCorpStoreSiteUnitTest()
        {
           _hifiCorpStoreSite = new HifiCorpStoreSite(new WebScrapper());
        }
        [Fact]
        public void GivenUrlWhenSearchingScrapThenReturnSiteContent()
        {
            var results = _hifiCorpStoreSite.Search("laptop");
            Assert.NotNull(results);
        }
    }
}