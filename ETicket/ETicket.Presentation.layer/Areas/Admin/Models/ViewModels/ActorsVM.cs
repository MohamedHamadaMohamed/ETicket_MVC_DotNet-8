using ETicket.Data.Acess.layer.Models;

namespace ETicket.Presentation.layer.Areas.Admin.Models.ViewModels
{
    public class ActorsVM
    {
        public List<Actor> actors { get; set; }

        public int TotalActorCount { get; set; }

        public int CurrentPageIndex { get; set; }
    }
}
