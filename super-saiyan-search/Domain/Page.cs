using System.Collections.Generic;
using SuperSaiyanSearch.Domain.Interfaces;

namespace SuperSaiyanSearch.Domain
{
    public class Page<E, T> where E : IEntity<T>
    {
        public IEnumerable<E> Items { get; set; }
        public T Next { get; set; }
        public T Previous { get; set; }
        public int Limit { get; set; }
        public int TotalResults { get; set; }
    }
}