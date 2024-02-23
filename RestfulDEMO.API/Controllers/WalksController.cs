using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestfulDEMO.API.Models.Domain;
using RestfulDEMO.API.Models.Dtos;
using RestfulDEMO.API.Repositories;

namespace RestfulDEMO.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IWalkRepository walkRepository;
        private readonly IMapper mapper;

        public WalksController(IWalkRepository walkRepository,IMapper mapper)
        {
            this.walkRepository = walkRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        // Route: GET/walks?filterOn=Name&filterQuery=Track&sortBy=Nme&isAscending=true&pageNumber=1&pageSize=10
        public async Task<ActionResult> GetAll(
            [FromQuery] string? filterOn,
            [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy,
            [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 1000
            )
        {
            var walksDomain = (await walkRepository.GetAllAsync(
                filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize));

            return Ok(mapper.Map<List<WalkDto>>(walksDomain));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walkDomainModel = await walkRepository.GetByIdAsync(id);
            if (walkDomainModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }


        [HttpPost]
        [CustomActionFilters.ValidateModelAttribute]
        // Route: POST/walks
        public async Task<ActionResult> CreateAsync([FromBody] AddWalksRequestDto addWalksRequestDto)
        {
            var walkDomainModel = mapper.Map<Walk>(addWalksRequestDto);
            await walkRepository.CreateAsync(walkDomainModel);

            return Ok(mapper.Map<WalkDto>(walkDomainModel));

        }

        [HttpPut]
        [Route("{id:Guid}")]
        [CustomActionFilters.ValidateModelAttribute]
        // Route: UPDATE/walk/id
        public async Task<IActionResult> UpdateById([FromRoute] Guid id, [FromBody] UpdateWalkRequestDto updateWalkRequestDto)
        {
            var existingWalkDomainModel = await walkRepository.GetByIdAsync(id);
            if (existingWalkDomainModel == null)
            {
                return NotFound();
            }

            await walkRepository.UpdateByIdAsync(
                id, mapper.Map<Walk>(updateWalkRequestDto)
                );

            return Ok(mapper.Map<WalkDto>(existingWalkDomainModel));
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        // Route: DELTE/walks/id
        public async Task<IActionResult> DeleteById([FromRoute] Guid id)
        {
            var existingWalksDomainModel = await walkRepository.GetByIdAsync(id);
            if (existingWalksDomainModel == null)
            {
                return NotFound();
            }

            await walkRepository.DeleteByIdAsync(id);

            return NoContent();
        }
    }
}
