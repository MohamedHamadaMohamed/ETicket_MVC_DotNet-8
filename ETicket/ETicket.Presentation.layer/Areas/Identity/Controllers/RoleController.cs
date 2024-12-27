using ETicket.Presentation.layer.Areas.Identity.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ETicket.Presentation.layer.Areas.Identity.Controllers
{
    [Area(nameof(Identity))]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            this._roleManager = roleManager;
        }
        public IActionResult Index(string? query = null, int PageNumber = 1)
        {
            ApplicationRolesVM applicationRolesVM = new ApplicationRolesVM();
            var roles = _roleManager.Roles.Select(
                e=>new ApplicationRoleVM()
                {
                    Id = e.Id,
                    Name = e.Name,
                });
            if (query != null)
            {
                query = query.Trim();
                roles = roles.Where(e => e.Name.Contains(query));
            }
            applicationRolesVM.TotalRoleCount = (roles.Count() + 4) / 5;
            if (PageNumber < 1) PageNumber = 1;
            roles = roles.Skip((PageNumber - 1) * 5).Take(5);
            applicationRolesVM.CurrentPageIndex = PageNumber;
            applicationRolesVM.roles = roles.ToList();
            return View(applicationRolesVM);
        }

        public IActionResult Create()
        {
            return View(new ApplicationRoleVM());
        }
        [HttpPost]
        public async Task< IActionResult> Create(ApplicationRoleVM applicationRoleVM)
        {
            ModelState.Remove("Id");
            if (ModelState.IsValid)
            {
                var result = await _roleManager.CreateAsync(new (roleName: applicationRoleVM.Name));
                
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(applicationRoleVM);
        }
    }
}
