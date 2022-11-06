namespace internship_registration.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool IsInstructor { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
