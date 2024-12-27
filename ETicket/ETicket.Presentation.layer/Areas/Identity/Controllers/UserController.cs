﻿using ETicket.Data.Acess.layer.Models;
using ETicket.Presentation.layer.Areas.Admin.Models.ViewModels;
using ETicket.Presentation.layer.Areas.Identity.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ETicket.Presentation.layer.Areas.Identity.Controllers
{
	[Area(nameof(Identity))]
	public class UserController : Controller
	{
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public IActionResult Index(string? query = null, int PageNumber = 1)
		{
            ApplicationUsersVM applicationUsersVM = new ApplicationUsersVM();
            var users = _userManager.Users.Select(
                e=> new ApplicationUserVM()
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Email = e.Email,
                    UserName = e.UserName,
                    DateOfBirth = e.DateOfBirth,
                    Gender = e.Gender,
                    Phone = e.PhoneNumber,
                    State = e.State,
                    City = e.City,
                    Region = e.Region,
                    Street = e.Street,
                    ProfilePicture = e.ProfilePicture?? "Profile.png",
                    IsAgree = e.IsAgree,
                    LockoutEnd = e.LockoutEnd,
                    LockoutEnabled = e.LockoutEnabled,
                    AccessFailedCount = e.AccessFailedCount,

                });
            if (query != null)
            {
                query = query.Trim();
                users = users.Where(e => e.FirstName.Contains(query));
            }


            applicationUsersVM.TotalUserCount = (users.Count() + 4) / 5;
            if (PageNumber < 1) PageNumber = 1;
            users = users.Skip((PageNumber - 1) * 5).Take(5);
            applicationUsersVM.CurrentPageIndex = PageNumber;
            applicationUsersVM.users = users.ToList();

            return View(applicationUsersVM);
		}
        public async Task<IActionResult> RoleManagement(string UserId)
        {
            var user =await _userManager.FindByIdAsync(UserId);
            if (user is not null)
            {
                var roles = _roleManager.Roles.ToList();
                UserRoleVM userRoleVM = new UserRoleVM();
                userRoleVM.Roles = roles;
                userRoleVM.User = user;
                return View(userRoleVM);
            }
            return View(user);
        }
        [HttpPost]
        public async Task< IActionResult> RoleManagement(string UserId, string RoleId)
        {
            if (ModelState.IsValid)
            {
				var user = await _userManager.FindByIdAsync(UserId);
                var role = await _roleManager.FindByIdAsync(RoleId);
                await _userManager.RemoveFromRolesAsync(user, await _userManager.GetRolesAsync(user));
                await _userManager.AddToRoleAsync(user, role.Name);

            }
            return RedirectToAction("Index"); 
        }

	}
}