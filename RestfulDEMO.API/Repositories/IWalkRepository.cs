using RestfulDEMO.API.Models.Domain;

namespace RestfulDEMO.API.Repositories
{
    public interface IWalkRepository
    {
        Task<List<Walk>> GetAllAsync();
        Task<Walk?> GetByIdAsync(Guid id);
        Task CreateAsync(Walk walk);
        Task UpdateByIdAsync(Guid id, Walk region);
        Task DeleteByIdAsync(Guid id);
    }
}
