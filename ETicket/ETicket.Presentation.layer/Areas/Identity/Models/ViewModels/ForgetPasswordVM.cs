using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ETicket.Presentation.layer.Areas.Identity.Models.ViewModels
{
	public class ForgetPasswordVM
	{
		[Required]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; } = string.Empty;
	}
}
