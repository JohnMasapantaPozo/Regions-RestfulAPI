using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using RestfulDEMO.API.Data;
using RestfulDEMO.API.Models.Domain;
using RestfulDEMO.API.Models.Dtos;
using RestfulDEMO.API.Repositories;
using System.Text.Json;

namespace RestfulDEMO.API.Controllers
{
    // htttps: //localhost:portnumber/api/regions
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
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
        private readonly IMapper mapper;
        private readonly ILogger<RegionsController> logger;

        public RegionsController(
            IRegionRepository repository, IMapper mapper, ILogger<RegionsController> logger)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.logger = logger;
        }


        [HttpGet]
        [Authorize(Roles = "Reader")]
        // Route: GET/regions/
        public async Task<ActionResult> GetAll()
        {
            try
            {
                //throw new Exception("This is a custom exception");

                // Logger
                logger.LogInformation(
                    "GetAll action method was invoked!!");

                //// Domain models
                //var regionsDomain = dbContext.Regions.ToList();

                //// 1. Cast Domain model to RegionDto
                ///
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

                // 2. OR using the Extension class:

                //var regionsDomain = (await repository.GetAllAsync())
                //    .Select(region => Extensions.RegionAsDto(region));
                //return Ok(regionsDomain);

                // 3. OR using the Mapping Automapper class:
                var regionsDomain = (await repository.GetAllAsync());

                logger.LogInformation(
                    $"Finished GetAll action method with data {JsonSerializer.Serialize(regionsDomain)}");

                return Ok(mapper.Map<List<RegionDto>>(regionsDomain));

            } catch (Exception ex)
            {
                logger.LogWarning(ex, ex.Message);
                throw;
            }           
        }


        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader")]
        // Route: GET/regions/id
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var region = await repository.GetByIdAsync(id);
            if (region == null)
            {
                return NotFound();
            }
            return Ok(
                //region.RegionAsDto()
                mapper.Map<RegionDto>(region)
                );
        }


        [HttpPost]
        [Authorize(Roles = "Writer")]
        [CustomActionFilters.ValidateModelAttribute]
        // Route: POST/regions
        public async Task<IActionResult> CreateRegion([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            var regionDomnainModel = mapper.Map<Region>(addRegionRequestDto);
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
                //regionDomnainModel.RegionAsDto()
                mapper.Map<RegionDto>(regionDomnainModel));
        }

        
        [HttpPut]
        [Route("{id:Guid}")]
        [CustomActionFilters.ValidateModelAttribute]
        [Authorize(Roles = "Writer")]
        // Route: UPDATE/regions/id
        public async Task<IActionResult> UpdateById([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            var existingRegionDomainModel = await repository.GetByIdAsync(id);
            if (existingRegionDomainModel == null)
            {
                return NotFound();
            }

            await repository.UpdateByIdAsync(
                id,
                mapper.Map<Region>(updateRegionRequestDto)
                //updateRegionRequestDto.DtoAsRegion()
                );

            return Ok(
                //existingRegionDomainModel.RegionAsDto()
                mapper.Map<RegionDto>(existingRegionDomainModel)
                );
        }
        

        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
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
