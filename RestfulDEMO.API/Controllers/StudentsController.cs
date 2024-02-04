using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RestfulDEMO.API.Controllers
{

    // htttps: //localhost:portnumber/api/students
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            string[] studens = new string[]
            {
                "JOhn", "Juan"
            };

            return Ok(studens);

        }
    }
}
