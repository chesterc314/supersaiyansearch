using System.Collections.Generic;

namespace SuperSaiyanSearch.Domain.Interfaces
{
    public interface IStoreSite
    {
        IEnumerable<IProduct> Search(string keyword);
    }
}