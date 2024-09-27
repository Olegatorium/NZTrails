using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Repository_contracts;

namespace NZWalksAPI.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext dbContext;

        public WalkRepository(NZWalksDbContext dbContext) 
        {
            this.dbContext = dbContext; 
        }

        public async Task<Walk> CreateAsync(Walk walk)
        {
            await dbContext.Walks.AddAsync(walk);

            await dbContext.SaveChangesAsync();

            return walk;
        }

        public async Task<List<Walk>> GetAllAsync()
        {
            return await dbContext.Walks.ToListAsync();
        }

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            return await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk?> DeleteAsync(Guid id)
        {
            Walk? walkToDelete = await GetByIdAsync(id);

            if (walkToDelete != null)
            {
                dbContext.Walks.Remove(walkToDelete);

                await dbContext.SaveChangesAsync();
            }

            return walkToDelete;
        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
        {
            Walk? walkToUpdate = await GetByIdAsync(id); 

            if (walkToUpdate != null)
            {
                walkToUpdate.Name = walk.Name;
                walkToUpdate.Description = walk.Description;
                walkToUpdate.LengthInKm = walk.LengthInKm;
                walkToUpdate.WalkImageUrl = walk.WalkImageUrl;
                walkToUpdate.DifficultyId = walk.DifficultyId;
                walkToUpdate.RegionId = walk.RegionId;
                walkToUpdate.Difficulty = walk.Difficulty;
                walkToUpdate.Region = walk.Region;

                await dbContext.SaveChangesAsync();
            }

            return walkToUpdate;
        }
    }
}
