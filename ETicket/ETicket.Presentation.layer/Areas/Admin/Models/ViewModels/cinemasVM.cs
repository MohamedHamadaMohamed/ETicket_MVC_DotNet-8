using ETicket.Data.Acess.layer.Models;

namespace ETicket.Presentation.layer.Areas.Admin.Models.ViewModels
{
    public class cinemasVM
    {
        public List<Cinema> Cinemas { get; set; }

        public int TotalCinemaCount { get; set; }

        public int CurrentPageIndex { get; set; }
    }
}
