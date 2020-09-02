using SuperSaiyanSearch.Integration.Interfaces;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using SuperSaiyanSearch.Api.Interfaces;

namespace SuperSaiyanSearch.Api
{
    public class ProductApi : IProductApi
    {
        private readonly IStoreSiteAggregation _storeSiteAggregation;
        private readonly IMapper _mapper;
        public ProductApi(IStoreSiteAggregation storeSiteAggregation, IMapper mapper)
        {
            _storeSiteAggregation = storeSiteAggregation;
            _mapper = mapper;
        }
        public ProductsReadDto Search(string keyword)
        {
            var productItems = _storeSiteAggregation
            .SearchAll(keyword)
            .Where(product => product.Name.ToLower().Contains(keyword.ToLower()));
            var products = _mapper.Map<IEnumerable<ProductReadDto>>(productItems).ToList();
            var productsReadDto = new ProductsReadDto(products);
            productsReadDto.TotalResults = products.Count;
            return productsReadDto;
        }
    }
}
