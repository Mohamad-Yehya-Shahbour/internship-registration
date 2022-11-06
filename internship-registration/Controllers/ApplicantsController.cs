using internship_registration.Data;
using internship_registration.Models;
using internship_registration.ViewModels;
//using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using MimeKit.Text;
using System.Net.Mail;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using Org.BouncyCastle.Utilities.Net;
using Microsoft.AspNetCore.Authorization;

namespace internship_registration.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ApplicantsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ApplicantsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("join")]
        public IActionResult JoinProgram(JoinProgramViewModel model)
        {
            var temp = _context.Applicants.FirstOrDefault(x => x.Email == model.Email && x.Program.Id == model.ProgramId);
            if (temp is not null)
                return BadRequest("already joined");
            var temp2 = _context.Applicants.FirstOrDefault(x => x.Email == model.Email);
            if (temp is not null)
                return BadRequest("email already exists");

            var applicantToAdd = new Applicant
            {
                Name = model.Name,
                Email = model.Email,
                MobileNumber = model.MobileNumber,
                University = model.University,
                Major = model.Major,
                CreationDate = DateTime.Now,
                GraduationDate = model.GraduationDate,
                ValidationToken = GenerateRandomToken(),
                IsValidated = false,
                ProgramId = model.ProgramId,
            };

            //send validation link
            //SendEmail(applicantToAdd.ValidationToken);

            //update current capacity
            var program = _context.Programs.FirstOrDefault(x => x.Id == model.ProgramId);
            if (program is not null)
            {
                if (program.MaxCapacity <= program.CurrentCapacity)
                    return BadRequest("program is full");
                program.CurrentCapacity ++;
                _context.SaveChanges();
            }

            _context.Applicants.Add(applicantToAdd);
            _context.SaveChanges();

            return Ok("created");
        }

        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            var applicants = _context.Applicants.Select(x => new
            {
                x.Id,
                x.Name,
                x.Email,
                x.MobileNumber,
                x.University,
                x.Major,
                x.CreationDate,
                x.GraduationDate,
                x.ValidationToken,
                x.IsValidated,
                ProgramName = x.Program.Title
            }).ToList();
            return Ok(applicants);
        }

        [HttpPost("Search")]
        [Authorize]
        public IActionResult Search(SearchViewModel model)
        {
            var applicants = _context.Applicants.Select(x => new
            {
                x.Id,
                x.Name,
                x.Email,
                x.MobileNumber,
                x.University,
                x.Major,
                x.CreationDate,
                x.GraduationDate,
                x.ValidationToken,
                x.IsValidated,
                ProgramName = x.Program.Title
            }).Where(y=>y.Name.Contains(model.Name) || y.Email.Contains(model.Name) || y.ProgramName.Contains(model.Name))
            .ToList();
            return Ok(applicants);
        }



        static string GenerateRandomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }

        static void SendEmail(string token)
        {

            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("mohamad.yehya.shahbur@gmail.com");
                mail.To.Add("mohamad.yehya.shahbur@gmail.com");
                mail.Subject = "Validate your Account to Join Internship Program";
                var url = "http://localhost:5272/Validator/" + token;
                mail.Body = "<a href="+ url + ">Click here to Validate your account</a>";
                mail.IsBodyHtml = true;
                SmtpClient smtp = new("smtp.gmail.com", 587);
                // put the sender Email and Password Here
                smtp.Credentials = new NetworkCredential("Sender@gmail.com", "SenderPassword");
                smtp.EnableSsl = true;
                smtp.Send(mail);
            }
        }

    }
}
