using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using RestfulDEMO.API.Data;
using RestfulDEMO.API.Models.Domain;

namespace RestfulDEMO.API.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly RestfulDbContextA dbContext;

        public SQLWalkRepository(RestfulDbContextA dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Walk>> GetAllAsync()
        {
            return await this.dbContext.Walks
                .Include("Difficulty").Include("Region")
                .ToListAsync();
        }

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            return await this.dbContext.Walks
                .Include("Difficulty").Include("Region")
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task CreateAsync(Walk walk)
        {
            await dbContext.Walks.AddAsync(walk);
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateByIdAsync(Guid id, Walk walk)
        {
            var existingWalk = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (existingWalk != null)
            {
                existingWalk.Name = walk.Name;
                existingWalk.Description = walk.Description;
                existingWalk.LengthInKm = walk.LengthInKm;
                existingWalk.WalkImageUrl = walk.WalkImageUrl;
                existingWalk.DifficultyId = walk.DifficultyId;
                existingWalk.RegionId = walk.RegionId;

                await dbContext.SaveChangesAsync();
            }
        }

        async Task IWalkRepository.DeleteByIdAsync(Guid id)
        {
            var existingWalk = await dbContext.Walks.FirstOrDefaultAsync(w => w.Id == id);
            if (existingWalk != null)
            {
                dbContext.Walks.Remove(existingWalk);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
