using ETicket.Business.Logic.layer.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace ETicket.Presentation.layer.Areas.Customer.Controllers
{
    [Area(nameof(Customer))]
    public class CategoryController : Controller
    {
        private ICategoryRepository _categoryRepository;
        private IMovieRepository _movieRepository;
        public CategoryController(ICategoryRepository categoryRepository, IMovieRepository movieRepository)
        {
            this._categoryRepository = categoryRepository;
            this._movieRepository = movieRepository;
        }
        public IActionResult Index()
        {
            var categories = _categoryRepository.Get(includeProps: [e => e.Movies]).ToList();
            return View(categories);
        }

        public IActionResult Details(int CategoryId)
        {
            var movies = _movieRepository.Get(filter: e => e.CategoryId == CategoryId, includeProps: [e => e.Category, e => e.Cinema]).ToList();
            return View(movies);
        }
    }
}
