using System.ComponentModel.DataAnnotations;

namespace internship_registration.ViewModels
{
    public class AddProgramViewModel
    {
        public string Title { get; set; } = null!;
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-mm-dd}")]

        public DateTime StartDate { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-mm-dd}")]

        public DateTime EndDate { get; set; }
        public int MaxCapacity { get; set; }
        public int CurrentCapacity { get; set; }
        public string ClassRoomCode { get; set; } = null!;
    }
}
