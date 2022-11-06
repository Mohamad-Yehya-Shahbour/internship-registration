using System.ComponentModel.DataAnnotations;

namespace internship_registration.Models
{
    public class ProgramUser
    {
        [Key]
        public int Id { get; set; }

        public int ProgramId { get; set; }
        public Program Program { get; set; } = null!;

        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
