using System.ComponentModel.DataAnnotations;

namespace ETicket.Presentation.layer.Areas.Identity.Models.ViewModels.ViewAccountModel
{
    public class ResetPasswordVM
    {
        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword))]
        public string ConfirmNewPassword { get; set; } = string.Empty;

        public string Email { get; set; } = null!;
        public string Token { get; set; } = null!;

    }
}
