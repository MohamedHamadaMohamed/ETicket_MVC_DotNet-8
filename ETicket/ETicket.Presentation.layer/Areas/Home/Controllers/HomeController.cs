using ETicket.Business.Logic.layer.Repository.IRepository;
using ETicket.Data.Acess.layer.Models;
using ETicket.Presentation.layer.Areas.Home.Models.ViewModels;
using ETicket.Presentation.layer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ETicket.Presentation.layer.Areas.Home.Controllers
{
    [Area(nameof(Home))]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IMovieRepository _movieRepository;
        public HomeController(IMovieRepository movieRepository, ILogger<HomeController> logger)
        {
            this._movieRepository = movieRepository;
            _logger = logger;
        }

        public IActionResult Index(string? query = null, int PageNumber = 1)
        {
            MoviesVM moviesVM = new MoviesVM();
            var movies = _movieRepository.Get(includeProps: [e => e.Cinema, e => e.Category]);
            if (query != null)
            {
                query = query.Trim();
                movies = movies.Where(e => e.Name.Contains(query) || e.Description.Contains(query));
            }
            moviesVM.TotalMovieCount = movies.Count()/5;
            if (PageNumber < 1) PageNumber = 1;
            movies = movies.Skip((PageNumber - 1) * 5).Take(5);
            moviesVM.CurrentPageIndex= PageNumber;
            moviesVM.movies = movies.ToList();
            //moviesVM.CurrentPageIndex = PageNumber;
            return View(moviesVM);

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
