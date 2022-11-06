using AuthenticationPlugin;
using internship_registration.Data;
using internship_registration.Models;
using Microsoft.AspNetCore.Http;


using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace internship_registration.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private IConfiguration _configuration;
        private readonly AuthService _auth;
        public AuthController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            _auth = new AuthService(_configuration);
        }

        [HttpPost("Register")]
        public IActionResult Register([FromBody] AuthUser user)
        {
            var userWithSameEmail = _context.AuthUsers.Where(u => u.Email == user.Email).SingleOrDefault();// it will return a single element if a match was found or null if its not found
            if (userWithSameEmail != null)
            {
                return BadRequest("User with same email already exist");
            }

            //string uniqueId = Guid.NewGuid().ToString();
            //string destination = Path.Combine("wwwroot", "pictures", uniqueId + ".jpg");
            //using (FileStream fs = new FileStream(destination, FileMode.Create))
            //{
            //    user.fromFile.CopyTo(fs);
            //}
            //string dbUrl = destination.Replace("wwwroot", "").Replace("\\", "/");

            var userObj = new AuthUser
            {
                Email = user.Email,
                Password = SecurePasswordHasherHelper.Hash(user.Password),
                //ImageUrl = dbUrl,
                // add all  other properties from your model and give it values
            };
            _context.AuthUsers.Add(userObj);
            _context.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);

        }


        [HttpPost("Login")]
        public IActionResult Login([FromBody] AuthUser user)
        {
            var userEmail = _context.AuthUsers.FirstOrDefault(u => u.Email == user.Email);
            if (userEmail == null)// if there is no such email
            {
                return NotFound("There is no such Email!");
            }

            if (!SecurePasswordHasherHelper.Verify(user.Password, userEmail.Password)) // we have used the useremail to access the paremeters in the database table users
            {
                return Unauthorized("Your Password is Wrong !");
            } // if the password and the hashed password in the database is not the same return unauthorized

            var claims = new[]
             {
               new Claim(JwtRegisteredClaimNames.Email, user.Email),
               new Claim(ClaimTypes.Email, user.Email),
               //new Claim(ClaimTypes.Role,userEmail.Role)
             };
            var token = _auth.GenerateAccessToken(claims);
            return new ObjectResult(new
            {
                access_token = token.AccessToken,
                expires_in = token.ExpiresIn,
                token_type = token.TokenType,
                creation_Time = token.ValidFrom,
                expiration_Time = token.ValidTo,
                User_id = userEmail.Id,
            }); // this return will show the access_token and many other things  if you want you can delete the rest and just keep the access token and you can add stuff like id for example
        }


    }
}
