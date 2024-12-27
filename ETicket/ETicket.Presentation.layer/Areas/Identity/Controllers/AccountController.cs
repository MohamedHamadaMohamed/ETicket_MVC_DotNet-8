using ETicket.Data.Acess.layer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ETicket.Presentation.layer.Areas.Identity.Models.ViewModels;
using Mono.TextTemplating;
using System.IO;
using ETicket.Utility.Utilities;

namespace ETicket.Presentation.layer.Areas.Identity.Controllers
{
    [Area(nameof(Identity))]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._roleManager = roleManager;
        }
        public IActionResult Register()
        {
            return View(new RegisterVM());
        }
        [HttpPost]
		public async Task< IActionResult> Register(RegisterVM registerVM)
		{
            ModelState.Remove("ProfilePicture");
            if (ModelState.IsValid)
            {
                ApplicationUser user = new()
                {
                    UserName = registerVM.Email.Split('@')[0],
					FirstName = registerVM.FirstName,
                    LastName = registerVM.LastName,
                    Email = registerVM.Email,
					IsAgree = registerVM.IsAgree,
                    PhoneNumber = registerVM.PhoneNumber,  
                    ProfilePicture = "Profile.png"
                };
                var result =await _userManager.CreateAsync(user,registerVM.Password);
                if(result.Succeeded)
                {
					await _userManager.AddToRoleAsync(user, SD.CustomerRole);
					return RedirectToAction(nameof(Login));
                }
                else
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
			return View(registerVM);
		}
		public IActionResult Login()
		{
			return View(new LoginVM());
		}
        [HttpPost]
		public async Task<IActionResult> Login(LoginVM loginVM)
		{
            if (ModelState.IsValid)
            {
                var user =await _userManager.FindByEmailAsync(loginVM.Email);
                if (user != null)
                {
                    var flag =await _userManager.CheckPasswordAsync(user,loginVM.Password);
                    if (flag)
                    {
                        await _signInManager.PasswordSignInAsync(user, loginVM.Password,isPersistent:loginVM.RemeberMe,false);
						return RedirectToAction("Index", "Home", new { area = "Home" });


					}
					else
                    {
						ModelState.AddModelError(string.Empty, "Email or password is in correct");

					}
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Email or password is in correct");

				}

            }
            
			return View(loginVM);
		}
		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("Index","Home",new {area="Home"});
		}

        public async Task< IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);
            ProfileVM profileVM;
            if (user is not null)
            {
                profileVM = new ProfileVM()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    DateOfBirth = user.DateOfBirth,
                    Gender = user.Gender,
                    State = user.State,
                    City = user.City,
                    Region = user.Region,
                    Street = user.Street,
                    ProfilePicture = user.ProfilePicture?? "Profile.png",
                    Phone = user.PhoneNumber
                };
                return View(profileVM);
            }
            return RedirectToAction("Login", "Account", new { area = "Identity" });
           



            
        }
        [HttpPost]
        public async Task<IActionResult> Profile(ProfileVM profileVM,IFormFile file)
        {
            var oldProfile = await _userManager.GetUserAsync(User);
            if(oldProfile !=null)
            {
                if (file != null && file.Length > 0)
                {
                    profileVM.ProfilePicture = Utility.Utilities.Utility.UploadFile(file, "Profiles");
                    if(oldProfile.ProfilePicture != "Profile.png")
                    {
                        Utility.Utilities.Utility.DeleteFile(oldProfile.ProfilePicture,"Profiles");
                    }
                }
                else
                {
                    profileVM.ProfilePicture = oldProfile.ProfilePicture;
                }

                oldProfile.FirstName = profileVM.FirstName;
                oldProfile.LastName = profileVM.LastName;
                oldProfile.Email = profileVM.Email;
                oldProfile.DateOfBirth = profileVM.DateOfBirth;
                oldProfile.Gender = profileVM.Gender;
                oldProfile.State = profileVM.State;
                oldProfile.City = profileVM.City;
                oldProfile.Region = profileVM.Region;
                oldProfile.Street = profileVM.Street;
                oldProfile.PhoneNumber = profileVM.Phone;
                oldProfile.ProfilePicture = profileVM.ProfilePicture;
                
                await _userManager.UpdateAsync(oldProfile);
            }
            else
            {
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }
            return View(profileVM);
        }

        public IActionResult ChangePassword()
        {
            return View(new ChangePasswordVM());
        }
        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordVM changePasswordVM)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.GetUserAsync(User).Result;
                if (user != null)
                {
                    var result = _userManager.ChangePasswordAsync(user, changePasswordVM.OldPassword, changePasswordVM.NewPassword);
                    if(result.IsCompletedSuccessfully)
                    {
						return RedirectToAction("Profile", "Account", new { area = "Identity" });
					}
				}
                else
                {
                    return RedirectToAction("Login", "Account", new { area = "Identity" });

                }
            }
            return View(changePasswordVM);
        }
        public IActionResult ForgetPassword()
        {
            return View(new ForgetPasswordVM());
        }
        [HttpPost]
		public IActionResult ForgetPassword(ForgetPasswordVM forgetPasswordVM )
		{
            if (ModelState.IsValid)
            {
                var user = _userManager.FindByEmailAsync(forgetPasswordVM.Email);
                if (user != null)
                {
                    Email email = new Email()
                    {
                        To = forgetPasswordVM.Email,
                        Subject = "Reset Password",
                        Body = ""



                    };
                         

				}
                else
                {
                    ModelState.AddModelError(string.Empty,"Email is not Exist");
                }
            }

			return View(forgetPasswordVM);
		}
	}
}
