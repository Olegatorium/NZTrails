using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Repository_contracts;
using System.Linq;

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
            walk.Id = new Guid();

            await dbContext.Walks.AddAsync(walk);

            await dbContext.SaveChangesAsync();

            return walk;
        }

        public async Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000)
        {
            var walks = dbContext.Walks.Include(x => x.Difficulty).Include(x => x.Region).AsQueryable();

            // Filtering
            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery)) 
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }
                else if (filterOn.Equals("Description", StringComparison.OrdinalIgnoreCase)) 
                {
                    walks = walks.Where(x => x.Description.Contains(filterQuery));
                }
            }

            // Sorting
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                switch (sortBy.ToLower())
                {
                    case "name":
                        walks = isAscending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
                        break;
                    case "description":
                        walks = isAscending ? walks.OrderBy(x => x.Description) : walks.OrderByDescending(x => x.Description);
                        break;
                    case "id":
                        walks = isAscending ? walks.OrderBy(x => x.Id) : walks.OrderByDescending(x => x.Id);
                        break;
                    case "length":
                        walks = isAscending ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);
                        break;
                }
            }

            // Pagination
            int skipResults = (pageNumber - 1) * pageSize;

            return await walks.Skip(skipResults).Take(pageSize).ToListAsync();
        }

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            return await dbContext.Walks.Include(x => x.Difficulty).Include(x => x.Region).FirstOrDefaultAsync(x => x.Id == id);
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

                await dbContext.SaveChangesAsync();
            }

            return walkToUpdate;
        }
    }
}
