using ETicket.Business.Logic.layer.Repository.IRepository;
using ETicket.Data.Acess.layer.Models;
using ETicket.Presentation.layer.Areas.Admin.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ETicket.Presentation.layer.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class CinemaController : Controller
    {
        private ICinemaRepository _cinemaRepository;
        private IMovieRepository _movieRepository;
        public CinemaController(ICinemaRepository cinemaRepository, IMovieRepository movieRepository)
        {
            this._cinemaRepository = cinemaRepository;
            this._movieRepository = movieRepository;
        }
        public IActionResult Index(string? query = null, int PageNumber = 1)
        {
            var cinemas = _cinemaRepository.Get(includeProps: [e => e.Movies]);
            cinemasVM cinemasVM = new cinemasVM();

            if (query != null)
            {
                query = query.Trim();

                cinemas = cinemas.Where(e => e.Name.Contains(query));
            }
            cinemasVM.TotalCinemaCount = (cinemas.Count() + 4) / 5;
            if (PageNumber < 1) PageNumber = 1;
            cinemas = cinemas.Skip((PageNumber - 1) * 5).Take(5);
            cinemasVM.CurrentPageIndex = PageNumber;
            cinemasVM.Cinemas = cinemas.ToList();







            return View(cinemasVM);
        }
        public IActionResult SCinema(int CinemaId)
        {

            //var movies = _dbContext.Movies.Where(e => e.CinemaId == CinemaId).Include(e => e.Category).Include(e => e.Cinema).ToList();
            var movies = _movieRepository.Get(filter: e => e.CinemaId == CinemaId, includeProps: [e => e.Category, e => e.Cinema]).ToList();
            return View(movies);
        }
        public IActionResult Create()
        {

            return View(new Cinema());
        }
        [HttpPost]
        public async Task<IActionResult> Create(Cinema cinema)
        {
            ModelState.Remove("Movies");
            if (ModelState.IsValid)
            {
                await _cinemaRepository.CreateAsync(cinema);
                return RedirectToAction(nameof(Index));
            }
            return View(cinema);
        }
        public IActionResult Edit(int CinemaId)
        {

            var cinema = _cinemaRepository.GetOne(filter: e => e.Id == CinemaId) as Cinema;
            return View(cinema);
        }
        [HttpPost]
        public IActionResult Edit(Cinema cinema)
        {
            ModelState.Remove("Movies");

            if (ModelState.IsValid)
            {
                _cinemaRepository.Update(cinema);
                return RedirectToAction(nameof(Index));
            }
            return View(cinema);
        }
        public IActionResult Delete(int CinemaId)
        {
            var cinema = _cinemaRepository.GetOne(filter: e => e.Id == CinemaId) as Cinema;
            if (cinema != null)
            {
                _cinemaRepository.Delete(cinema);
            }
            return RedirectToAction(nameof(Index));
        }




    }
}
