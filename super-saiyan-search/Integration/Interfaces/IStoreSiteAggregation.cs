using System.Collections.Generic;
using SuperSaiyanSearch.Domain.Interfaces;

namespace SuperSaiyanSearch.Integration.Interfaces
{
    public interface IStoreSiteAggregation
    {
        IEnumerable<IProduct> SearchAll(string keyword);
    }
}