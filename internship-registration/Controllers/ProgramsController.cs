using internship_registration.Data;
using internship_registration.Models;
using internship_registration.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Digests;

namespace internship_registration.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProgramsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ProgramsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult Get(int id)
        {
            var program = _context.Programs
                .Select(x => new
                {
                    x.Id,
                    x.Title,
                    x.StartDate,
                    x.EndDate,
                    x.ClassRoomCode,
                    x.MaxCapacity,
                    x.CurrentCapacity,
                    Applicants = x.Applicants.Select(a=>a.Name).ToArray(),
                    Instructors = x.ProgramUsers.Select(x=>x.User.Name).ToArray()
                }).FirstOrDefault(x => x.Id == id);
            return Ok(program);
        }
        [HttpGet("GetAll")]
        [Authorize]
        public IActionResult GetAll()
        {
            var programs = _context.Programs
                .Select(x => new
                {
                    x.Id,
                    x.Title,
                    x.StartDate,
                    x.EndDate,
                    x.ClassRoomCode,
                    x.MaxCapacity,
                    x.CurrentCapacity,
                    Applicants = x.Applicants.Select(a => a.Name).ToArray(),
                    Instructors = x.ProgramUsers.Select(x => x.User.Name).ToArray()
                });
            return Ok(programs);
        }


        [HttpPost]
        [Authorize]
        public IActionResult Add(AddProgramViewModel programViewModel)
        {
            Models.Program program = new()
            {
                Title = programViewModel.Title,
                StartDate = programViewModel.StartDate,
                EndDate = programViewModel.EndDate,
                ClassRoomCode = programViewModel.ClassRoomCode,
                MaxCapacity = programViewModel.MaxCapacity,
                CurrentCapacity = programViewModel.CurrentCapacity
            };

            _context.Programs.Add(program);
            _context.SaveChanges();
            return Ok(program);
        }
    }
}
