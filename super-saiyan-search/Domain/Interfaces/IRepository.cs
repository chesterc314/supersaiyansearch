using System;
using System.Collections.Generic;
using System.Linq.Expressions;
namespace SuperSaiyanSearch.Domain.Interfaces
{
    public interface IRepository<E, T> where E : IEntity<T>
    {
        E Get(T id);
        E Get(Expression<Func<E, bool>> predicate);
        IEnumerable<E> GetAll();
        IEnumerable<E> GetAll(Expression<Func<E, bool>> predicate);
        void Update(E entity);
        void UpdateAll(IEnumerable<E> entities);
        E Create(E entity);
        IEnumerable<E> CreateAll(IEnumerable<E> entities);
        void Remove(T id);
        void RemoveAll(IEnumerable<T> ids);
        int Count(Expression<Func<E, bool>> predicate);
    }
}