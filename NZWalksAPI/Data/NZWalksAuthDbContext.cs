using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZWalksAPI.Data
{
    public class NZWalksAuthDbContext : IdentityDbContext
    {
        public NZWalksAuthDbContext(DbContextOptions<NZWalksAuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            string clientId = "CC632C7D-F494-4EEF-80BD-67AC9DC2C12D";
            string adminId = "9190F681-435D-4020-A695-0416429B9935";

            List<IdentityRole> roles = new List<IdentityRole>
            {
               new IdentityRole
               {
                  Id = clientId,
                  ConcurrencyStamp = clientId,
                  Name = "Client",
                  NormalizedName = "Client".ToUpper()
               },

               new IdentityRole
               {
                  Id = adminId,
                  ConcurrencyStamp = adminId,
                  Name = "Admin",
                  NormalizedName = "Admin".ToUpper()
               }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
 