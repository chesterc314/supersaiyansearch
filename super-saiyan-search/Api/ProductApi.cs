using System;
using SuperSaiyanSearch.Domain;
using SuperSaiyanSearch.Domain.Interfaces;
using SuperSaiyanSearch.Integration;

namespace SuperSaiyanSearch.Api
{
    public class ProductApi : IProductApi
    {
        private readonly IStoreSiteAggregation _storeSiteAggregation;
        private readonly IPageRepository<Product, Guid> productRepository;
        public ProductApi(IStoreSiteAggregation storeSiteAggregation)
        {
            _storeSiteAggregation = storeSiteAggregation;
        }
        public ProductsReadDto Search(string keyword)
        {
            throw new NotImplementedException();
        }
    }
}
