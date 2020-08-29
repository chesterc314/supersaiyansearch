using System.Linq;
using SuperSaiyanSearch.Domain.Interfaces;
using Xunit;

namespace SuperSaiyanSearch.Integration.Tests
{
    public class PickNPayStoreSiteUnitTest
    {
        private readonly IStoreSite _pickNPayStoreSite;

        public PickNPayStoreSiteUnitTest()
        {
           _pickNPayStoreSite = new PickNPayStoreSite(new WebScrapper());
        }
        [Fact]
        public void GivenUrlWhenSearchingScrapThenReturnSiteContent()
        {
            var results = _pickNPayStoreSite.Search("bread");
            Assert.Equal(true, results.All(r => r.Price > 0));
            Assert.NotNull(results);
        }
    }
}