using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using RestfulDEMO.API.Data;
using RestfulDEMO.API.Models.Domain;
using RestfulDEMO.API.Models.Dtos;
using RestfulDEMO.API.Repositories;

namespace RestfulDEMO.API.Controllers
{
    // htttps: //localhost:portnumber/api/regions
    [ApiController]
    [Route("api/[controller]")]
    public class RegionsController : ControllerBase
    {
        /* 
         * Deprecated in favor of the Regions SQL repository class.
         
        private readonly RestfulDbContextA dbContext;
        public RegionsController(RestfulDbContextA dbContext)
        {
            this.dbContext = dbContext;
        }
        */

        private readonly IRegionRepository repository;

        public RegionsController(IRegionRepository repository)
        {
            this.repository = repository;
        }

        
        [HttpGet]
        // Route: GET/regions/id
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
            var regionsDto = (await repository.GetAllAsync()).Select(
                region => Extensions.RegionAsDto(region)
                );

            return Ok(regionsDto);
        }


        [HttpGet]
        [Route("{id:Guid}")]
        // Route: GET/regions/id
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //var region = this.dbContext.Regions.Find(id);
            var region = await repository.GetByIdAsync(id);
            if (region == null)
            {
                return NotFound();
            }
            return Ok(region.RegionAsDto());
        }

        
        [HttpPost]
        // Route: POST/regions
        public async Task<IActionResult> CreateRegion([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            var regionDomnainModel = new Region
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl,
            };

            await repository.CreateAsync(regionDomnainModel);

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

        
        [HttpPut]
        [Route("{id:Guid}")]
        // Route: UPDATE/regions/id
        public async Task<IActionResult> UpdateById([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            var existingRegionDomainModel = await repository.GetByIdAsync(id);
            if (existingRegionDomainModel == null)
            {
                return NotFound();
            }

            await repository.UpdateByIdAsync(id, updateRegionRequestDto.DtoAsRegion());
            return Ok(existingRegionDomainModel.RegionAsDto());
        }
        

        [HttpDelete]
        [Route("{id:Guid}")]
        // Route: DELTE/regions/id
        public async Task<IActionResult> DeleteteById([FromRoute] Guid id)
        {
            var existingRegionDomainModel = await repository.GetByIdAsync(id);
            if (existingRegionDomainModel == null)
            {
                return NotFound();
            }

            await repository.DeleteteByIdAsync(id);

            return NoContent();
        }
    }
}
