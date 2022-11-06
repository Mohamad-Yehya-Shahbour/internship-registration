using internship_registration.Data;
using internship_registration.Models;
using internship_registration.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace internship_registration.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            var users = _context.Users
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                    x.Email,
                    x.IsInstructor,
                    Programs = x.ProgramUsers.Select(x => x.Program.Title).ToArray()
                }).ToList();
            return Ok(users);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(UserViewModel user)
        {
            var temp = _context.Users.FirstOrDefault(x => x.Email == user.Email);
            if (temp is not null)
                return BadRequest("this Email already exists");

            User user1 = new()
            {
                Email = user.Email,
                Name = user.Name,
                IsInstructor = user.IsInstructor,
                CreationDate = DateTime.Now
            };
            _context.Users.Add(user1);
            _context.SaveChanges();
            return Ok(user);
        }

        [HttpPut]
        [Authorize]
        public IActionResult Edit(UserViewModel user)
        {
            var temp = _context.Users.FirstOrDefault(x => x.Id == user.Id);
            if (temp is null)
                return BadRequest("this user does not exists");

            User user1 = new()
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                IsInstructor = user.IsInstructor,
                CreationDate = DateTime.Now
            };

            _context.Users.Update(user1);
            _context.SaveChanges();
            return Ok(user1);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            var temp = _context.Users.FirstOrDefault(x => x.Id == id);
            if (temp is null)
                return BadRequest("cannot find this user");
            _context.Users.Remove(temp);
            _context.SaveChanges();
            return Ok();
        }

    }
}
