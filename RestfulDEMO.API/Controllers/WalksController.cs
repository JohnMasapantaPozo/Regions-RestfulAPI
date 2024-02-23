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
        // Route: GET/walks/
        public async Task<ActionResult> GetAll()
        {
            var walksDomain = (await walkRepository.GetAllAsync());
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
        // Route: POST/walks
        public async Task<ActionResult> CreateAsync([FromBody] AddWalksRequestDto addWalksRequestDto)
        {
            if (ModelState.IsValid)
            {
                var walkDomainModel = mapper.Map<Walk>(addWalksRequestDto);
                await walkRepository.CreateAsync(walkDomainModel);

                return Ok(mapper.Map<WalkDto>(walkDomainModel));
            } else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("{id:Guid}")]
        // Route: UPDATE/walk/id
        public async Task<IActionResult> UpdateById([FromRoute] Guid id, [FromBody] UpdateWalkRequestDto updateWalkRequestDto)
        {
            if (ModelState.IsValid)
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
            else
            {
                return BadRequest();
            }
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
