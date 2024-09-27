using AutoMapper;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO.Region;

namespace NZWalksAPI.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
           CreateMap<Region, RegionResponseDto>().ReverseMap();
        }
    }
}
