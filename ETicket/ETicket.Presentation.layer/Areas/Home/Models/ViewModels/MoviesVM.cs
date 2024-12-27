using ETicket.Data.Acess.layer.Models;

namespace ETicket.Presentation.layer.Areas.Home.Models.ViewModels
{
    public class MoviesVM
    {
        public List<Movie> movies {  get; set; }

        public int TotalMovieCount { get; set; }

        public int CurrentPageIndex { get; set; }
    }
}
