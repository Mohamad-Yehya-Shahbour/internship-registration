using System.ComponentModel.DataAnnotations;

namespace internship_registration.Models
{
    public class Program
    {

        [Key]
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int MaxCapacity { get; set; }
        public int CurrentCapacity { get; set; }
        public string  ClassRoomCode { get; set; } = null!;
        

        public ICollection<ProgramUser> ProgramUsers { get; set; } = null!;
        public ICollection<Applicant> Applicants { get; set; } = null!;
    }
}
