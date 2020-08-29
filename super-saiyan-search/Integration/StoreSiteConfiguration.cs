
using System.Collections.Generic;
using SuperSaiyanSearch.Domain.Interfaces;
using SuperSaiyanSearch.Integration.Interfaces;

namespace SuperSaiyanSearch.Integration
{
    public class StoreSiteConfiguration : IStoreSiteConfiguration
    {
        private readonly IWebScrapper _webScrapper;
        private readonly IHttpClient _httpClient;

        public StoreSiteConfiguration(IWebScrapper webScrapper, IHttpClient httpClient)
        {
            _webScrapper = webScrapper;
            _httpClient = httpClient;
        }
        public IEnumerable<IStoreSite> StoreSites
        {
            get
            {
                return new List<IStoreSite> {
                    new TakealotStoreSite(_httpClient),
                    new GameStoreSite(_webScrapper),
                    new IncredibleConnectionStoreSite(_webScrapper),
                    new MakroStoreSite(_webScrapper),
                    new HifiCorpStoreSite(_webScrapper),
                    new CheckersStoreSite(_webScrapper),
                    new ShopriteStoreSite(_webScrapper),
                    new PickNPayStoreSite(_webScrapper),
                    new ClicksStoreSite(_webScrapper),
                    new DischemStoreSite(_webScrapper),
                    new MatrixWarehouseStoreSite(_webScrapper)
                };
            }
        }
    }
}
