using SuperSaiyanSearch.Domain.Interfaces;
using System.Linq;
using Xunit;

namespace SuperSaiyanSearch.Integration.Tests
{
    public class MatrixWarehouseStoreSiteUnitTest
    {
        private readonly IStoreSite _matrixWarehouseStoreSite;

        public MatrixWarehouseStoreSiteUnitTest()
        {
           _matrixWarehouseStoreSite = new MatrixWarehouseStoreSite(new WebScrapper());
        }
        [Fact]
        public void GivenUrlWhenSearchingScrapThenReturnSiteContent()
        {
            var results = _matrixWarehouseStoreSite.Search("laptop");
            var hasPositivePrices = true;
            Assert.NotNull(results);
            Assert.Equal(hasPositivePrices, results.All(r => r.Price > 0));
        }
    }
}