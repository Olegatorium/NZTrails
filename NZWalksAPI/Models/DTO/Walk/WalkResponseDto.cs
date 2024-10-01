using NZWalks.API.Models.DTO;
using NZWalksAPI.Models.DTO.Region;
using System.ComponentModel.DataAnnotations;

namespace NZWalksAPI.Models.DTO.Walk
{
    public class WalkResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }

        //Navigation properties:
        public RegionResponseDto Region { get; set; }
        public DifficultyDto Difficulty { get; set; }
    }
}
