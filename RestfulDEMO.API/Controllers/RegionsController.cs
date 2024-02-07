using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestfulDEMO.API.Data;
using RestfulDEMO.API.Models.Domain;

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

        [HttpGet]
        public IActionResult GetAll()
        {
            var regions = dbContext.Regions.ToList();
            return Ok(regions);
        }
    }
}
