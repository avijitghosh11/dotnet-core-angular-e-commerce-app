using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductsToReturnDto>()
                .ForMember(x=>x.ProductType , y=>y.MapFrom(z=>z.ProductType.Name))
                .ForMember(x => x.ProductBrand, y => y.MapFrom(z => z.ProductBrand.Name))
                .ForMember(x=>x.PictureUrl, y=>y.MapFrom<ProductUrlResolver>());
        }
    }
}
