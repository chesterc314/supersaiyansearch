using System.Collections.Generic;
using System.Linq;
using SuperSaiyanSearch.Domain.Interfaces;

namespace SuperSaiyanSearch.Integration
{
    public class StoreSiteAggregation : IStoreSiteAggregation
    {
        private readonly IStoreSiteConfiguration _storeSiteConfiguration;
        public StoreSiteAggregation(IStoreSiteConfiguration storeSiteConfiguration)
        {
            _storeSiteConfiguration = storeSiteConfiguration;
        }
        public IEnumerable<IProduct> SearchAll(string keyword)
        {
            IEnumerable<IStoreSite> storeSites = _storeSiteConfiguration.StoreSites;
            IEnumerable<IProduct> products = storeSites.SelectMany(storeSite => storeSite.Search(keyword));
            return products;
        }
    }
}
