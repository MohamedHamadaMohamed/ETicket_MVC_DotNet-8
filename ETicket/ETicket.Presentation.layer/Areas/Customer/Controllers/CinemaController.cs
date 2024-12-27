using ETicket.Business.Logic.layer.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace ETicket.Presentation.layer.Areas.Customer.Controllers
{
    [Area(nameof(Customer))]
    public class CinemaController : Controller
    {
        private ICinemaRepository _cinemaRepository;
        private IMovieRepository _movieRepository;
        public CinemaController(ICinemaRepository cinemaRepository, IMovieRepository movieRepository)
        {
            this._cinemaRepository = cinemaRepository;
            this._movieRepository = movieRepository;
        }
        public IActionResult Index()
        {
            //var cinemas = _dbContext.Cinemas.Include(e => e.Movies).ToList();
            var cinemas = _cinemaRepository.Get(includeProps: [e => e.Movies]).ToList();
            return View(cinemas);
        }
        public IActionResult Details(int CinemaId)
        {

            //var movies = _dbContext.Movies.Where(e => e.CinemaId == CinemaId).Include(e => e.Category).Include(e => e.Cinema).ToList();
            var movies = _movieRepository.Get(filter: e => e.CinemaId == CinemaId, includeProps: [e => e.Category, e => e.Cinema]).ToList();
            return View(movies);
        }
    }
}
