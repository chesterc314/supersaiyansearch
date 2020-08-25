using System;
using SuperSaiyanSearch.Domain;
using SuperSaiyanSearch.Domain.Interfaces;
using SuperSaiyanSearch.Integration.Interfaces;

namespace SuperSaiyanSearch.Api
{
    public class ProductApi : IProductApi
    {
        private readonly IStoreSiteAggregation _storeSiteAggregation;
        private readonly IPageRepository<Product, Guid> _productRepository;
        public ProductApi(IStoreSiteAggregation storeSiteAggregation, IPageRepository<Product, Guid> productRepository)
        {
            _storeSiteAggregation = storeSiteAggregation;
            _productRepository = productRepository;
        }
        public ProductsReadDto Search(string keyword)
        {
            throw new NotImplementedException();
        }
    }
}
