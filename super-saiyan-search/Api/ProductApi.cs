using System;
using SuperSaiyanSearch.Domain;
using SuperSaiyanSearch.Domain.Interfaces;
using SuperSaiyanSearch.Integration.Interfaces;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace SuperSaiyanSearch.Api
{
    public class ProductApi : IProductApi
    {
        private readonly IStoreSiteAggregation _storeSiteAggregation;
        private readonly IPageRepository<Product, Guid> _productRepository;
        private readonly IMapper _mapper;
        public ProductApi(IStoreSiteAggregation storeSiteAggregation, IPageRepository<Product, Guid> productRepository, IMapper mapper)
        {
            _storeSiteAggregation = storeSiteAggregation;
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public ProductsReadDto Search(string keyword, string next, string previous, int? limit)
        {
            if (!this.ProductExits(keyword))
            {
                var productItems = _storeSiteAggregation.SearchAll(keyword);
                _productRepository.CreateAll(productItems.Select(product => new Product(product)));
            }

            Guid? nextGuid = null;
            Guid? previousGuid = null;
            if (next != null)
            {
                nextGuid = Guid.Parse(next);
            }
            if (previous != null)
            {
                previousGuid = Guid.Parse(previous);
            }

            var page = this._productRepository.GetPage(product => product.Name.Contains(keyword), nextGuid.Value, previousGuid.Value, limit);
            var products = _mapper.Map<IEnumerable<ProductReadDto>>(page.Items);
            var productsReadDto = new ProductsReadDto(products);
            productsReadDto.Next = page.Next.ToString();
            productsReadDto.Previous = page.Previous.ToString();
            productsReadDto.Limit = page.Limit;
            productsReadDto.TotalResults = page.TotalResults;

            return productsReadDto;
        }
        private bool ProductExits(string keyword)
        {
            return this._productRepository.Count(product => product.Name.Contains(keyword)) > 0;
        }
    }
}
