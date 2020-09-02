using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SuperSaiyanSearch.Domain.Interfaces;
using SuperSaiyanSearch.Integration.Interfaces;

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
            var products = new List<IProduct>();
            Parallel.ForEach(storeSites, storeSite =>
            {
                products.AddRange(storeSite.Search(keyword));
            });
            return products;
        }
    }
}
