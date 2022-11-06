using internship_registration.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace internship_registration.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ValidatorController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ValidatorController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{token}")]
        public IActionResult Validate(string token)
        {
            var applicant = _context.Applicants.FirstOrDefault(x => x.ValidationToken == token);
            if (applicant is null)
                return BadRequest("token not found");

            if(applicant.IsValidated == true)
                return BadRequest("already validated");

            applicant.IsValidated = true;
            _context.SaveChanges();
            return Ok();
        }



    }
}
