using AutoMapper;
using NZWalks.API.Models.DTO;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO.Region;
using NZWalksAPI.Models.DTO.Walk;

namespace NZWalksAPI.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            //Region:
            CreateMap<Region, RegionResponseDto>().ReverseMap();
            CreateMap<Region, RegionAddRequestDto>().ReverseMap();
            CreateMap<Region, RegionUpdateRequestDto>().ReverseMap();

            //Walk:
            CreateMap<Walk, WalkResponseDto>().ReverseMap();
            CreateMap<Walk, WalkAddRequestDto>().ReverseMap();
            CreateMap<Walk, WalkUpdateRequestDto>().ReverseMap();

            //Difficulty:
            CreateMap<Difficulty, DifficultyDto>().ReverseMap();
        }
    }
}
