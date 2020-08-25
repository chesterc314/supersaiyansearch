using System;
using System.Linq.Expressions;

namespace SuperSaiyanSearch.Domain.Interfaces
{
    public interface IPageRepository<E, T> : IRepository<E, T> where E : IEntity<T>
    {
        Page<E, T> GetPage(Expression<Func<E, bool>> predicate, T next, T previous, int? limit);
    }
}