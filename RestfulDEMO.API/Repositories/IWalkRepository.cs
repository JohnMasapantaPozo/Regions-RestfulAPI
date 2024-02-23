using RestfulDEMO.API.Models.Domain;
using System.Globalization;

namespace RestfulDEMO.API.Repositories
{
    public interface IWalkRepository
    {
        Task<List<Walk>> GetAllAsync(
            string? filterOn = null,
            string? filterQuery = null,
            string? sortBy = null,
            bool isAscending = true,
            int pageNumber = 1,
            int pageSize = 1000
            );
        Task<Walk?> GetByIdAsync(Guid id);
        Task CreateAsync(Walk walk);
        Task UpdateByIdAsync(Guid id, Walk region);
        Task DeleteByIdAsync(Guid id);
    }
}
