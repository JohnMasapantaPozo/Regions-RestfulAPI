using RestfulDEMO.API.Models.Domain;
using RestfulDEMO.API.Models.Dtos;

namespace RestfulDEMO.API.Repositories
{
    public interface IRegionRepository
    {
        /* The methods applied to the database originally in the controllers class
         * have now to be moved to our repository class.
         * 
         * Thin interface provide the definition solely of such interface that will
         * perform operation to a database.
         */

        Task<List<Region>> GetAllAsync();
        Task<Region?> GetByIdAsync(Guid id);
        Task CreateAsync(Region region);
        Task UpdateByIdAsync(Guid id, Region region);
        Task DeleteteByIdAsync(Guid id);
    }
}