using ETicket.Data.Acess.layer.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace ETicket.Data.Acess.layer.Models
{
    public class ApplicationUser : IdentityUser
    {
        
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public DateTime? DateOfBirth { get; set; }
        public Gender? Gender { get; set; }

        public string? State { get; set; }
        public string? City { get; set; }

        public string? Region { get; set; }
        public string? Street { get; set; }

        public string? ProfilePicture { get; set; }
		public bool IsAgree { get; set; }

	}
}
