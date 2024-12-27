namespace ETicket.Presentation.layer.Areas.Identity.Models.ViewModels
{
    public class ApplicationRolesVM
    {
        public List<ApplicationRoleVM> roles { get; set; }

        public int TotalRoleCount { get; set; }

        public int CurrentPageIndex { get; set; }
    }
}
