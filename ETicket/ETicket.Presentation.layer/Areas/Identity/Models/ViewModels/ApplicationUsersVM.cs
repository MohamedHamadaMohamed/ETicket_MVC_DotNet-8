using ETicket.Data.Acess.layer.Models;

namespace ETicket.Presentation.layer.Areas.Identity.Models.ViewModels
{
    public class ApplicationUsersVM
    {
        public List<ApplicationUserVM> users { get; set; }

        public int TotalUserCount { get; set; }

        public int CurrentPageIndex { get; set; }
    }
}
