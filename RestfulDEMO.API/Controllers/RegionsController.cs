using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using RestfulDEMO.API.Data;
using RestfulDEMO.API.Models.Domain;
using RestfulDEMO.API.Models.Dtos;

namespace RestfulDEMO.API.Controllers
{

    // htttps: //localhost:portnumber/api/regions
    [ApiController]
    [Route("api/[controller]")]
    public class RegionsController : ControllerBase
    {

        // Hardcoded return
        //[HttpGet]
        //public IActionResult GetRegions()
        //{
        //var regions = new List<Region>
        //{
        //    new Region
        //    {
        //        Id = Guid.NewGuid(),
        //        Name = "Bergen",
        //        Code = "5007",
        //        RegionImageUrl = "image/bergen/url"
        //    },
        //    new Region
        //    {
        //        Id = Guid.NewGuid(),
        //        Name = "Oslo",
        //        Code = "0572",
        //        RegionImageUrl = "image/oslo/url"
        //    }
        //};

        //return Ok(regions);
        //}

        private readonly RestfulDbContextA dbContext;

        public RegionsController(RestfulDbContextA dbContext)
        {
            this.dbContext = dbContext;
        }

        // Route: GET/regions/id
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            //// Domain models
            //var regionsDomain = dbContext.Regions.ToList();

            //// Cast Domain model to RegionDto
            //var regionsDto = new List<RegionDto>();
            //foreach (var region in regionsDomain)
            //{
            //    regionsDto.Add(
            //        new RegionDto()
            //        {
            //            Id = region.Id,
            //            Code = region.Code,
            //            Name = region.Name,
            //            RegionImageUrl = region.RegionImageUrl,
            //        }
            //    );
            //}

            // OR using the Extension class:
            var regionsDto = (await dbContext.Regions.ToListAsync()).Select(
                region => Extensions.RegionAsDto(region)
                );

            return Ok(regionsDto);
        }

        // Route: GET/regions/id
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //var region = this.dbContext.Regions.Find(id);
            var region = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (region == null)
            {
                return NotFound();
            }
            return Ok(region.RegionAsDto());
        }

        // Route: POST/regions
        [HttpPost]
        public async Task<IActionResult> CreateRegion([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            var regionDomnainModel = new Region
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl,
            };

            await this.dbContext.Regions.AddAsync(regionDomnainModel);
            await this.dbContext.SaveChangesAsync();

            /* CreatedAtAction:
                Heps us creating the location URL where our newly created resurce can be found.

                Response e.g., 

                content-type: application/json; charset=utf-8 
                date: Sat20 Jan 2024 12:38:08 GMT 
                location: https://localhost:7008/api/Regions/9da108a6-28f9-41c2-fefa-08dc2806196d 
                server: Kestrel
            */

            return CreatedAtAction(
                nameof(GetById),
                new { id = regionDomnainModel.Id },
                regionDomnainModel.RegionAsDto()
                );
        }

        // Route: UPDATE/regions/id
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateById([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            var existingRegionDomainModel = await this.dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingRegionDomainModel == null)
            {
                return NotFound();
            }

            existingRegionDomainModel.Code = updateRegionRequestDto.Code;
            existingRegionDomainModel.Name = updateRegionRequestDto.Name;
            existingRegionDomainModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;

            await this.dbContext.SaveChangesAsync();
            return Ok(existingRegionDomainModel.RegionAsDto());

        }

        // Route: DELTE/regions/id
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteteById([FromRoute] Guid id)
        {
            var existingRegionDomainModel = await this.dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingRegionDomainModel == null)
            {
                return NotFound();
            }

            this.dbContext.Remove(existingRegionDomainModel);
            await this.dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
