using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO.Region;
using NZWalksAPI.Repository_contracts;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            _regionRepository = regionRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "Client,Admin")]
        public async Task<ActionResult<IEnumerable<RegionResponseDto>>> GetAllRegions() 
        {
            List<Region> regionDomain = await _regionRepository.GetAllAsync();

            // Map Domain Models to DTOs
            List<RegionResponseDto> regionsDto = _mapper.Map<List<RegionResponseDto>>(regionDomain);

            return Ok(regionsDto);
        }

        [HttpGet("{regionId}")]
        [Authorize(Roles = "Client,Admin")]
        public async Task<ActionResult<RegionResponseDto>> GetRegionById([FromRoute] Guid regionId) 
        {
            Region? regionDomain = await _regionRepository.GetByIdAsync(regionId);

            if (regionDomain == null) 
            {
                return NotFound();
            }

            RegionResponseDto regionDto = _mapper.Map<RegionResponseDto>(regionDomain);

            return Ok(regionDto);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<RegionResponseDto>> CreateRegion([FromBody]RegionAddRequestDto regionAddRequest) 
        {
            if (regionAddRequest == null)
            {
                return BadRequest("Request data is missing.");
            }

            Region regionDomain = _mapper.Map<Region>(regionAddRequest);

            regionDomain = await _regionRepository.CreateAsync(regionDomain);

            RegionResponseDto regionResponse = _mapper.Map<RegionResponseDto>(regionDomain);

            return CreatedAtAction(nameof(GetRegionById), new { regionId = regionResponse.Id }, regionResponse);
        }

        [HttpDelete("{regionId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteRegion([FromRoute]Guid regionId) 
        {
            Region? regionDomain = await _regionRepository.DeleteAsync(regionId);

            if (regionDomain == null) 
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPut("{regionId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<RegionResponseDto>> UpdateRegion([FromRoute]Guid regionId, [FromBody]RegionUpdateRequestDto regionUpdateRequest)
        {
            if (regionUpdateRequest == null)
            {
                return BadRequest("Request data is missing.");
            }

            Region? regionDomain = _mapper.Map<Region>(regionUpdateRequest);

            regionDomain = await _regionRepository.UpdateAsync(regionId, regionDomain);

            if (regionDomain == null) 
            {
                return NotFound();
            }

            RegionResponseDto regionResponse = _mapper.Map<RegionResponseDto>(regionDomain);

            return CreatedAtAction(nameof(GetRegionById), new { regionId = regionId }, regionResponse);
        }
    }
}
