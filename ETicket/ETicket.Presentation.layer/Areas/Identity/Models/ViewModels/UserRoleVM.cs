using ETicket.Data.Acess.layer.Models;
using Microsoft.AspNetCore.Identity;

namespace ETicket.Presentation.layer.Areas.Identity.Models.ViewModels
{
	public class UserRoleVM
	{
		public ApplicationUser User {get;set; }
		public List<IdentityRole> Roles { get; set; }
		

	}
}
