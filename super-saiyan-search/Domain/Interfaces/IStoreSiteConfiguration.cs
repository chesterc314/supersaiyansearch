using System.Collections.Generic;

namespace SuperSaiyanSearch.Domain.Interfaces
{
    public interface IStoreSiteConfiguration
    {
        IEnumerable<IStoreSite> StoreSites { get; }
    }
}