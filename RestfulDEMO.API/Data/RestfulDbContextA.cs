using Microsoft.EntityFrameworkCore;
using RestfulDEMO.API.Models.Domain;

namespace RestfulDEMO.API.Data
{
    public class RestfulDbContextA: DbContext
    {
        /* Context Class:
         * Represents a session with the database and provides a set op API's to performa database operations.
         * It funtions as a bridge between the controllers and the database.
         */

        public RestfulDbContextA(DbContextOptions dbContextOptions): base(dbContextOptions)
        {
                        
        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
    }
}
