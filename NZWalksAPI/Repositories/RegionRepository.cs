using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Repository_contracts;

namespace NZWalksAPI.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext dbContext;
        public RegionRepository(NZWalksDbContext dbContext) 
        {
            this.dbContext = dbContext;
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await dbContext.Regions.AddAsync(region);

            await dbContext.SaveChangesAsync();

            return region;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await dbContext.Regions .ToListAsync();
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            Region? regionToDelete = await GetByIdAsync(id);

            if (regionToDelete != null) 
            {
                dbContext.Regions.Remove(regionToDelete);

                await dbContext.SaveChangesAsync();
            }

            return regionToDelete;
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            Region? regionToUpdate = await GetByIdAsync(id);

            if (regionToUpdate != null) 
            {
                regionToUpdate.Name = region.Name;
                regionToUpdate.Code = region.Code;
                regionToUpdate.RegionImageUrl = region.RegionImageUrl;

                await dbContext.SaveChangesAsync();
            }

            return regionToUpdate;
        }
    }
}
