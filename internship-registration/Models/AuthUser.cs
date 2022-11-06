using System.ComponentModel.DataAnnotations;

namespace internship_registration.Models
{
    public class AuthUser
    {
        [Key]
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
