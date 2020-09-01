using AutoMapper;
using SuperSaiyanSearch.Domain;

namespace SuperSaiyanSearch.Api
{
    public class ProductProfile: Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductReadDto>();
        }
        
    }
}