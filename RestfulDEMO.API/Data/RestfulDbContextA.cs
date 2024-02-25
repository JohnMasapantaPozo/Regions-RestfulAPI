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

        public RestfulDbContextA(
            DbContextOptions<RestfulDbContextA> dbContextOptions): base(dbContextOptions)
        {
                        
        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data for difficulties

            var difficulties = new List<Difficulty>()
            {
                new Difficulty()
                {
                    Id = Guid.Parse("c66e04fd-ba85-423f-8314-2cae09983a59"),
                    Name = "Easy",
                },
                new Difficulty()
                {
                    Id = Guid.Parse("58dfb073-0e35-4b40-ada5-efd3b36974eb"),
                    Name = "Medium",
                },
                new Difficulty()
                {
                    Id = Guid.Parse("7b0c6301-0a03-4e1b-9021-8419ab0ef7d4"),
                    Name = "Hard",
                }
            };

            // Sed difficulties to the database
            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            // seed data for Regions
            var regions = new List<Region>
            {
                new Region
                {
                    Id = Guid.Parse("e4e46541-1ce3-4ffe-8f89-f07f0259450f"),
                    Name = "Bergen",
                    Code = "BRG",
                    RegionImageUrl = "http/bergen/picture"
                },
                new Region
                {
                    Id = Guid.Parse("4c55f1bf-2af7-4463-8b55-e991e7377c4f"),
                    Name = "Oslo",
                    Code = "OSL",
                    RegionImageUrl = "http/oslo/picture"
                },
                new Region
                {
                    Id = Guid.Parse("8671f7ab-9c94-44ba-bdd4-b3d9e0dc4261"),
                    Name = "Stavanger",
                    Code = "STV",
                    RegionImageUrl = "http/stavanger/picture"
                },
            };

            // Sed regions to the database 
            modelBuilder.Entity<Region>().HasData(regions);
        }
    }
}
