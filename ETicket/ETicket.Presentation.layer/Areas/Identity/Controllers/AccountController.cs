using ETicket.Data.Acess.layer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ETicket.Presentation.layer.Areas.Identity.Models.ViewModels;
using Mono.TextTemplating;
using System.IO;
using ETicket.Utility.Utilities;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using ETicket.Presentation.layer.Areas.Identity.Models.ViewModels.ViewAccountModel;

namespace ETicket.Presentation.layer.Areas.Identity.Controllers
{
    [Area(nameof(Identity))]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailSender _emailSender;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, IEmailSender emailSender)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._roleManager = roleManager;
            this._emailSender = emailSender;
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
                    
					return RedirectToAction("Profile", "Account", new { area = "Identity" });
					
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
		public async Task< IActionResult> ForgetPassword(ForgetPasswordVM forgetPasswordVM )
		{
            if (ModelState.IsValid)
            {
                var user =await _userManager.FindByEmailAsync(forgetPasswordVM.Email);
                if (user != null)
                {
                    var token =await _userManager.GeneratePasswordResetTokenAsync(user);
                    var ResetPasswordLink = Url.Action("ResetPassword","Account",new {email=user.Email,Token=token},Request.Scheme);
                   await _emailSender.SendEmailAsync(
						email: forgetPasswordVM.Email,
                        subject: "Reset Password",
						 message: ResetPasswordLink);
                        return RedirectToAction(nameof(CkeckYourMail));
                         

				}
                else
                {
                    ModelState.AddModelError(string.Empty,"Email is not Exist");
                }
            }

			return View(forgetPasswordVM);
		}
	    public IActionResult CkeckYourMail()
        {
            return View();
        }

        public IActionResult ResetPassword(string email ,string token)
        {
            ResetPasswordVM resetPasswordVM = new ResetPasswordVM()
            {
                Email = email,
                Token = token
            };
            
            return View(resetPasswordVM);
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM resetPasswordVM)
        {
            if (ModelState.IsValid)
            {
                
                var user = await _userManager.FindByEmailAsync(resetPasswordVM.Email);
                if (user != null)
                {
                  var result =  await _userManager.ResetPasswordAsync(user, resetPasswordVM.Token, resetPasswordVM.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(Login));
                    }
                    else
                    {
                        foreach(var error in  result.Errors)
                        {
                            ModelState.AddModelError(string.Empty,error.Description);

                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "email is not exists ");
                }
            }
            return View(resetPasswordVM);

        }
        public async Task< IActionResult> Lock(string UserId)
        {
             var user =await _userManager.FindByIdAsync(UserId);
             user.LockoutEnabled = false;
            await _userManager.UpdateAsync(user);
            
            return RedirectToAction("Index","User",new {area="Identity"});
        }
        public async Task<IActionResult> UnLock(string UserId)
        {
            var user =await _userManager.FindByIdAsync(UserId);
            user.LockoutEnabled = true;
            await _userManager.UpdateAsync(user);

            return RedirectToAction("Index", "User", new { area = "Identity" });
        }
    }
}
