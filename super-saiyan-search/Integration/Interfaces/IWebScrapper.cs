using HtmlAgilityPack;

namespace SuperSaiyanSearch.Integration.Interfaces
{
    public interface IWebScrapper
    {
        HtmlDocument Scrap(string url);
    }
}