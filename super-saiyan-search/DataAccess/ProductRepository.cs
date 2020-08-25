using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using SuperSaiyanSearch.Domain;
using SuperSaiyanSearch.Domain.Interfaces;
using System.Linq;

namespace SuperSaiyanSearch.DataAccess
{
    public class ProductRepository : IPageRepository<Product, Guid>
    {
        private readonly ProductContext _productContext;
        public ProductRepository(ProductContext productContext)
        {
            _productContext = productContext;
        }
        public int Count(Expression<Func<Product, bool>> predicate)
        {
            return _productContext.Products.Count(predicate);
        }

        public Product Create(Product entity)
        {
            _productContext.Products.Add(entity);
            _productContext.SaveChanges();
            return entity;
        }

        public IEnumerable<Product> CreateAll(IEnumerable<Product> entities)
        {
            _productContext.Products.AddRange(entities);
            _productContext.SaveChanges();
            return entities;
        }

        public Product Get(Guid id)
        {
            return _productContext.Products.FirstOrDefault(product => product.Id == id);
        }

        public Product Get(Expression<Func<Product, bool>> predicate)
        {
            return _productContext.Products.FirstOrDefault(predicate);
        }

        public IEnumerable<Product> GetAll()
        {
            return _productContext.Products.Where(p => true).ToList();
        }

        public IEnumerable<Product> GetAll(Expression<Func<Product, bool>> predicate)
        {
            return _productContext.Products.Where(predicate).ToList();
        }

        public Page<Product, Guid> GetPage(Expression<Func<Product, bool>> predicate, Guid next, Guid previous, int? limit)
        {
            var defaultLimit = limit ?? 10;
            var page = new Page<Product, Guid>();
            IQueryable<Product> pageQuery = _productContext.Products.Where(predicate).OrderByDescending(p => p.Id).Take(defaultLimit);
            IQueryable<Product> nextQuery = _productContext.Products.Where(p => p.Id.CompareTo(next) < 0).Where(predicate).OrderByDescending(p => p.Id).Take(defaultLimit);
            IQueryable<Product> previousQuery = _productContext.Products.Where(p => p.Id.CompareTo(next) > 0).Where(predicate).OrderByDescending(p => p.Id).Take(defaultLimit);
            
            if (next != null)
            {
                pageQuery = nextQuery;
                page.Next = pageQuery.Last().Id;
            }
            else if (previous != null)
            {
                pageQuery = previousQuery;
                page.Previous = pageQuery.First().Id;
            }
            else
            {
                pageQuery = _productContext.Products.Where(predicate).OrderByDescending(p => p.Id).Take(defaultLimit);
                page.Next = pageQuery.Last().Id;
                page.Previous = pageQuery.First().Id;
            }
            page.Items = pageQuery.ToList();
            page.Limit = defaultLimit;
            page.TotalResults = this.Count(predicate);

            return page;
        }

        public void Remove(Guid id)
        {
            var product = this.Get(id);
            if (product != null)
            {
                _productContext.Products.Remove(product);
                _productContext.SaveChanges();
            }
        }

        public void RemoveAll(IEnumerable<Guid> ids)
        {
            var products = ids.Select(id => this.Get(id));
            _productContext.Products.RemoveRange(products);
            _productContext.SaveChanges();
        }

        public void Update(Product entity)
        {
            _productContext.Products.Update(entity);
            _productContext.SaveChanges();
        }

        public void UpdateAll(IEnumerable<Product> entities)
        {
            _productContext.Products.UpdateRange(entities);
            _productContext.SaveChanges();
        }
    }
}