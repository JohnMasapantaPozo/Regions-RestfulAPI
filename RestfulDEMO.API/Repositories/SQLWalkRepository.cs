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

        public async Task<List<Walk>> GetAllAsync(
            string? filterOn = null,
            string? filterQuery = null,
            string? sortBy = null,
            bool isAscending = true,
            int pageNumber = 1,
            int pageSize = 1000
            )
        {
            //return await this.dbContext.Walks
            //    .Include("Difficulty").Include("Region")
            //    .ToListAsync();

            var walks = this.dbContext.Walks
                .Include("Difficulty").Include("Region")
                .AsQueryable();
            
            // Filtering
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }
                else if (filterOn.Equals("Description", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Description.Contains(filterQuery));
                }
                else
                {
                    // add more filtering suntionality here
                    walks = walks;
                }
            }

            // Sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending? walks.OrderBy(x => x.Name): walks.OrderByDescending(x => x.Name);
                }
                else if (sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);
                }
                else
                {
                    // add more sorting funtionality here
                    walks = walks;
                }
            }

            // Pagination
            var skipResults = (pageNumber - 1) * pageSize;

            return await walks.Skip(skipResults).Take(pageSize).ToListAsync();
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
