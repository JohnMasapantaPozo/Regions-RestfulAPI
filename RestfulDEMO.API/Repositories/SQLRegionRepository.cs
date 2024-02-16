using Microsoft.EntityFrameworkCore;
using RestfulDEMO.API.Data;
using RestfulDEMO.API.Models.Domain;
using RestfulDEMO.API.Models.Dtos;

namespace RestfulDEMO.API.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly RestfulDbContextA dbContext;

        public SQLRegionRepository(RestfulDbContextA dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<Region>> GetAllAsync()
        {
            return await this.dbContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetByIdAsync( Guid id)
        {
            return await this.dbContext.Regions.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task CreateAsync(Region region)
        {
            await dbContext.Regions.AddAsync(region);
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateByIdAsync(Guid id, Region region)
        {
            var existingRegion = await dbContext.Regions.FirstOrDefaultAsync(r => r.Id == id);
            if (existingRegion != null)
            {
                existingRegion.Code = region.Code;
                existingRegion.Name = region.Name;
                existingRegion.RegionImageUrl = region.RegionImageUrl;

                await dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteteByIdAsync(Guid id)
        {
            var existingRegion = await dbContext.Regions.FirstOrDefaultAsync(r => r.Id == id);
            if (existingRegion != null)
            {
                dbContext.Remove(existingRegion);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
