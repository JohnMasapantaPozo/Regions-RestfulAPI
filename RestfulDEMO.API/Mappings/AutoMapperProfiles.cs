using AutoMapper;
using RestfulDEMO.API.Models.Domain;
using RestfulDEMO.API.Models.Dtos;

namespace RestfulDEMO.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<AddRegionRequestDto, Region>().ReverseMap();
            CreateMap<UpdateRegionRequestDto, Region>().ReverseMap();

            CreateMap<Walk, WalkDto>().ReverseMap();
            CreateMap<AddWalksRequestDto, Walk>().ReverseMap();
            CreateMap<UpdateWalkRequestDto, Walk>().ReverseMap();
            
            CreateMap<Difficulty, DifficultyDto>().ReverseMap();
        }
    }
}
