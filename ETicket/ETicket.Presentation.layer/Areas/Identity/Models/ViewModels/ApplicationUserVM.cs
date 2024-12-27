using ETicket.Data.Acess.layer.Models.Enums;

namespace ETicket.Presentation.layer.Areas.Identity.Models.ViewModels
{
    public class ApplicationUserVM
    {
        public string Id { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public  string? Email { get; set; }
        public  string? UserName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public Gender? Gender { get; set; }
        public string? Phone { get; set; }

        public string? State { get; set; }
        public string? City  { get; set; }

        public string? Region { get; set; }
        public string? Street { get; set; }

        public string? ProfilePicture { get; set; }
        public bool IsAgree { get; set; }

        public  DateTimeOffset? LockoutEnd { get; set; }
        public  bool LockoutEnabled { get; set; }
        public  int AccessFailedCount { get; set; }
        public string? Role { get; set; }



    }

}
