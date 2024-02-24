using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace RestfulDEMO.API.Data
{
    public class RestFulAuthDbContext : IdentityDbContext
    {
        public RestFulAuthDbContext(
            DbContextOptions<RestFulAuthDbContext> dbAuthContextOptions) : base(dbAuthContextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed roles to the auth database
            var readerId = "b09efdef-d99d-422d-9ab4-39a9caa38c1b";
            var writerId = "e938b487-0396-48c3-a15c-969a5901b141";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = readerId,
                    ConcurrencyStamp = readerId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper(),
                },
                new IdentityRole
                {
                    Id = writerId,
                    ConcurrencyStamp = writerId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper(),
                }
            };

            // Seed roles to the auth database
            modelBuilder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
