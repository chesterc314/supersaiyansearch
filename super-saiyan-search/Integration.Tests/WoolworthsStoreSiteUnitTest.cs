using System;
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
           _woolworthsStoreSite = new WoolworthsStoreSite();
        }
        [Fact]
        public void GivenUrlWhenSearchingScrapThenReturnSiteContent()
        {
            var results = _woolworthsStoreSite.Search("bread");
            Assert.Equal(true, results.All(r => r.Price > 0));
            Assert.NotNull(results);
        }
    }
}