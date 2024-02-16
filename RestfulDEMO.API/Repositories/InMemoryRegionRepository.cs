using RestfulDEMO.API.Models.Domain;

namespace RestfulDEMO.API.Repositories
{
    public class InMemoryRegionRepository : IRegionRepository
    {
        public async Task<List<Region>> GetAllAsync()
        {
            return new List<Region>
            {
                new Region()
                {
                    Id = Guid.NewGuid(),
                    Name = "Bergen",
                    Code = "5007",
                    RegionImageUrl = "image/bergen/url"
                },
                new Region
                {
                    Id = Guid.NewGuid(),
                    Name = "Oslo",
                    Code = "0572",
                    RegionImageUrl = "image/oslo/url"
                }
            };
        }

        Task IRegionRepository.CreateAsync(Region region)
        {
            throw new NotImplementedException();
        }

        Task IRegionRepository.DeleteteByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        Task<Region?> IRegionRepository.GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        Task IRegionRepository.UpdateByIdAsync(Guid id, Region region)
        {
            throw new NotImplementedException();
        }
    }
}
