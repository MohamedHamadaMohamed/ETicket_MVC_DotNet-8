using System.ComponentModel.DataAnnotations;

namespace ETicket.Presentation.layer.Areas.Identity.Models.ViewModels
{
	public class LoginVM
	{
		[Required(ErrorMessage = "Email is required")]
		[EmailAddress(ErrorMessage = "Email Invalid")]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; } = string.Empty;

		[Required(ErrorMessage = "Password is required")]
		[DataType(DataType.Password)]
		public string Password { get; set; } = null!;

		public bool RemeberMe { get; set; }


	}
}
