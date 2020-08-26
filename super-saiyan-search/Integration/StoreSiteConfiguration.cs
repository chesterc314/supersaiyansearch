
using System.Collections.Generic;
using SuperSaiyanSearch.Domain.Interfaces;

namespace SuperSaiyanSearch.Integration
{
    public class StoreSiteConfiguration : IStoreSiteConfiguration
    {
        public IEnumerable<IStoreSite> StoreSites
        {
            get
            {
                return new List<IStoreSite> { new TakealotStoreSite() };
            }
        }
    }
}
