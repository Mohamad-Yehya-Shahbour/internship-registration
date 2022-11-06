using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace internship_registration.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool IsInstructor { get; set; }
        public DateTime CreationDate { get; set; }

        public ICollection<ProgramUser> ProgramUsers { get; set; } = null!;

    }
}
