
using HtmlAgilityPack;
using SuperSaiyanSearch.Integration.Interfaces;

namespace SuperSaiyanSearch.Integration
{
    public class WebScrapper : IWebScrapper
    {
        private readonly HtmlWeb _web;
        public WebScrapper()
        {
            _web = new HtmlWeb();
        }

        public HtmlDocument Scrap(string url)
        {
            return _web.Load(url);
        }
    }
}