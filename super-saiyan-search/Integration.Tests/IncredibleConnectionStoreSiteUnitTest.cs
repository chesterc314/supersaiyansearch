using System.Linq;
using SuperSaiyanSearch.Domain.Interfaces;
using Xunit;

namespace SuperSaiyanSearch.Integration.Tests
{
    public class IncredibleConnectionStoreSiteUnitTest
    {
        private readonly IStoreSite _incredibleConnectionStoreSite;

        public IncredibleConnectionStoreSiteUnitTest()
        {
           _incredibleConnectionStoreSite = new IncredibleConnectionStoreSite(new WebScrapper());
        }
        [Fact]
        public void GivenUrlWhenSearchingScrapThenReturnSiteContent()
        {
            var results = _incredibleConnectionStoreSite.Search("laptop");
            Assert.Equal(true, results.All(r => r.Price > 0));
            Assert.NotNull(results);
        }
    }
}