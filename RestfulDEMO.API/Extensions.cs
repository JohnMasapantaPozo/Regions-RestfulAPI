using RestfulDEMO.API.Models.Domain;
using RestfulDEMO.API.Models.Dtos;

namespace RestfulDEMO.API
{
    public static class Extensions
    {

        public static RegionDto RegionAsDto(this Region region)
        {
            return new RegionDto
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl,
            };
        }

        public static DifficultyDto DifficultyAsDto(this Difficulty difficulty)
        {
            return new DifficultyDto
            {
                Id = difficulty.Id,
                Name = difficulty.Name,
            };
        }

        public static WalkDto WalkAsDto(this Walk walk)
        {
            return new WalkDto
            {
                Id = walk.Id,
                Name = walk.Name,
                Description = walk.Description,
                LengthInKm = walk.LengthInKm,
                WalkImageUrl = walk.WalkImageUrl,
                DifficultyId = walk.DifficultyId,
                Difficulty = walk.Difficulty,
                Region = walk.Region,
            };
        }
    }
}
