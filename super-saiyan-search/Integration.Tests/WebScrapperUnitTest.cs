using SuperSaiyanSearch.Integration.Interfaces;
using Xunit;

namespace SuperSaiyanSearch.Integration.Tests
{
    public class WebScrapperUnitTest
    {
        private readonly IWebScrapper _scrapper;

        public WebScrapperUnitTest()
        {
            _scrapper = new WebScrapper();
        }
        [Fact]
        public void GivenUrlWhenSearchingScrapThenReturnSiteContent()
        {
            var result = _scrapper.Scrap("https://www.takealot.com/all?qsearch=laptop");
            Assert.Contains("Takealot", result.DocumentNode.SelectSingleNode("//head/title").InnerHtml);
        }
    }
}