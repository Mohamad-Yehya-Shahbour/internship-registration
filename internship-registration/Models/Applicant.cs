using System.ComponentModel.DataAnnotations;

namespace internship_registration.Models
{
    public class Applicant
    {

        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string MobileNumber { get; set; } = null!;
        public string University { get; set; } = null!;
        public string Major { get; set; } = null!;
        public DateTime GraduationDate { get; set; }
        public DateTime CreationDate { get; set; }
        public string ValidationToken { get; set; } = null!;
        public bool IsValidated { get; set; }

        public int ProgramId { get; set; }
        public Program Program { get; set; } = null!;
    }
}
