using System.ComponentModel.DataAnnotations;

namespace ETicket.Presentation.layer.Areas.Identity.Models.ViewModels
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "The First Name is required")]
        [MinLength(3, ErrorMessage = "the minimum length is 3 characters")]
        [MaxLength(255, ErrorMessage = "the maximum  length is 255 characters")]
        public string FirstName { get; set; } = string.Empty;
        [Required(ErrorMessage = "The Last Name is required")]
        [MinLength(3, ErrorMessage = "the minimum length is 3 characters")]
        [MaxLength(255, ErrorMessage = "the maximum  length is 255 characters")]
        public string LastName { get; set; } = String.Empty;
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email Invalid")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }= string.Empty;
        public string? PhoneNumber { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]

        public string Password { get; set; } = null!;
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = null!;

        public bool IsAgree { get; set; }

        public string ProfilePicture { get; set; } = null!;

	}
}
