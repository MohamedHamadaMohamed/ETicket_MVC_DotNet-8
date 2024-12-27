using System.ComponentModel.DataAnnotations;

namespace ETicket.Presentation.layer.Areas.Identity.Models.ViewModels
{
    public class ChangePasswordVM
    {
        [Required(ErrorMessage = "Old Password is required")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; } = null!;
        [Required(ErrorMessage = "New Password is required")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; } = string.Empty;
        [Required(ErrorMessage = "Confirm New Password is required")]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword))]
        public string ConfirmNewPassword { get; set; } =string.Empty;

    }
}
