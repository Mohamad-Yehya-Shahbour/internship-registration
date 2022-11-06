using System.ComponentModel.DataAnnotations;

namespace internship_registration.ViewModels
{
    public class JoinProgramViewModel
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string MobileNumber { get; set; } = null!;
        public string University { get; set; } = null!;
        public string Major { get; set; } = null!;
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-mm-dd}")]
        public DateTime GraduationDate { get; set; }
        public DateTime? CreationDate { get; set; }
        public string? ValidationToken { get; set; }
        public bool IsValidated { get; set; }
        public int ProgramId { get; set; }
    }
}
