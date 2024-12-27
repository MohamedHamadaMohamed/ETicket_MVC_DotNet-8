using ETicket.Business.Logic.layer.Repository.IRepository;
using ETicket.Data.Acess.layer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ETicket.Utility.Utilities;

namespace ETicket.Presentation.layer.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class MovieController : Controller
    {
        private IMovieRepository _movieRepository;
        private ICategoryRepository _categoryRepository;
        private ICinemaRepository _cinemaRepository;
        private IActorRepository _actorRepository;
        private IActorMovieRepository _actorMovieRepository;

        public MovieController(IMovieRepository movieRepository, ICategoryRepository categoryRepository, ICinemaRepository cinemaRepository, IActorRepository actorRepository, IActorMovieRepository actorMovieRepository)
        {
            this._movieRepository = movieRepository;
            this._categoryRepository = categoryRepository;
            this._cinemaRepository = cinemaRepository;
            this._actorRepository = actorRepository;
            this._actorMovieRepository = actorMovieRepository;
        }
        public IActionResult Index(string? query = null, int PageNumber = 1)
        {

            var movies = _movieRepository.Get(includeProps: [e => e.Cinema, e => e.Category]);
            if (query != null)
            {
                query = query.Trim();
                movies = movies.Where(e => e.Name.Contains(query) || e.Description.Contains(query));
            }
            ViewBag.MovieCount = (movies.Count()+4)/5;
            if (PageNumber < 1) PageNumber = 1;
            movies = movies.Skip((PageNumber - 1) * 5).Take(5);

            return View(movies.ToList());
        }
        public IActionResult Details(int movieId)
        {
            Movie? movie = _movieRepository.Get(filter: e => e.Id == movieId, includeProps: [e => e.Category, e => e.Cinema]).Include(m => m.ActorMovies).ThenInclude(ma => ma.Actors).FirstOrDefault() as Movie;

            var actors = movie.ActorMovies.Select(ma => ma.Actors).ToList();

            ViewBag.actors = actors;
            if (movie != null)
            {
                (movie.views)++;
                _movieRepository.Update(movie);
                return View(movie);
            }
            return NotFound();

        }
        
        public IActionResult NotFoundAnyThing()
        {
            return View();
        }
        public IActionResult Create()
        {
            var categories = _categoryRepository.Get().ToList();
            var cinemas = _cinemaRepository.Get().ToList();
            var actors = _actorRepository.Get().ToList();
            ViewBag.categories = categories;
            ViewBag.cinemas = cinemas;
            ViewBag.actors = actors;
            return View(new Movie());
        }
        [HttpPost]
        public async Task<IActionResult> Create(Movie movie, List<int> ActorsId, IFormFile file)
        {
            ModelState.Remove("ImgUrl");
            ModelState.Remove("Cinema");
            ModelState.Remove("Category");
            ModelState.Remove("Actors");
            ModelState.Remove("ActorMovies");



            if (ModelState.IsValid)
            {

                if (file != null && file.Length > 0)
                {
                    
                    movie.ImgUrl = Utility.Utilities.Utility.UploadFile(file, "movies");
                }

               await _movieRepository.CreateAsync(movie);
                foreach (var item in ActorsId)
                {
                  await  _actorMovieRepository.CreateAsync(new ActorMovie { ActorsId = item, MoviesId = movie.Id });

                }
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }
        public IActionResult Edit(int MovieId)
        {
            var categories = _categoryRepository.Get().ToList();
            var cinemas = _cinemaRepository.Get().ToList();
            var actors = _actorRepository.Get().ToList();
            ViewBag.categories = categories;
            ViewBag.cinemas = cinemas;
            ViewBag.actors = actors;
            var movie = _movieRepository.GetOne(filter: e => e.Id == MovieId);
            return View(movie);
        }
        [HttpPost]
        public IActionResult Edit(Movie movie, IFormFile file)
        {
            ModelState.Remove("ImgUrl");
            ModelState.Remove("Cinema");
            ModelState.Remove("Category");
            ModelState.Remove("Actors");
            ModelState.Remove("ActorMovies");
            if (ModelState.IsValid)
            {
                var oldMovie = _movieRepository.GetOne(filter: e => e.Id == movie.Id, trancked: false);
                if (file != null && file.Length > 0)
                {
                    

                    movie.ImgUrl = Utility.Utilities.Utility.UploadFile(file, "movies");
                    Utility.Utilities.Utility.DeleteFile(oldMovie.ImgUrl, "movies");

                }
                else
                {
                    movie.ImgUrl = oldMovie.ImgUrl;
                }


                _movieRepository.Update(movie);
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }
        public ActionResult Delete(int MovieId)
        {
            var movie = _movieRepository.GetOne(filter: e => e.Id == MovieId);


            Utility.Utilities.Utility.DeleteFile(movie.ImgUrl, "movies");


            if (movie != null)
            {
                _movieRepository.Delete(movie);
            }
            return RedirectToAction(nameof(Index));
        }



    }
}
