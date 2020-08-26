using HtmlAgilityPack;

namespace SuperSaiyanSearch.Integration
{
    public interface IWebScrapper
    {
        HtmlDocument Scrap(string url);
    }
}