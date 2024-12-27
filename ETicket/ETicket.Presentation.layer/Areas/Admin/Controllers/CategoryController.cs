using ETicket.Business.Logic.layer.Repository.IRepository;
using ETicket.Data.Acess.layer.Models;
using ETicket.Presentation.layer.Areas.Admin.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ETicket.Presentation.layer.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class CategoryController : Controller
    {
        private ICategoryRepository _categoryRepository;
        private IMovieRepository _movieRepository;
        public CategoryController(ICategoryRepository categoryRepository, IMovieRepository movieRepository)
        {
            this._categoryRepository = categoryRepository;
            this._movieRepository = movieRepository;
        }
        public IActionResult Index(string? query = null, int PageNumber = 1)
        {
            CategoriesVM categoriesVM = new CategoriesVM();
            var categories = _categoryRepository.Get(includeProps: [e => e.Movies]);


            if (query != null)
            {
                query = query.Trim();

                categories = categories.Where(e => e.Name.Contains(query) );
            }
            categoriesVM.TotalCategoryCount = (categories.Count() + 4) / 5;
            if (PageNumber < 1) PageNumber = 1;
            categories = categories.Skip((PageNumber - 1) * 5).Take(5);
            categoriesVM.CurrentPageIndex = PageNumber;
            categoriesVM.Categories = categories.ToList();




            return View(categoriesVM);
        }

        public IActionResult Details(int CategoryId)
        {
            var movies = _movieRepository.Get(filter: e => e.CategoryId == CategoryId, includeProps: [e => e.Category, e => e.Cinema]).ToList();
            return View(movies);
        }
        public IActionResult Create()
        {
            return View(new Category());
        }
        [HttpPost]
        public async Task< IActionResult> Create(Category category)
        {
            ModelState.Remove("Movies");
            if (ModelState.IsValid)
            {
               await _categoryRepository.CreateAsync(category);

                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }
        public IActionResult Edit(int CategoryId)
        {
            var category = _categoryRepository.GetOne(filter: e => e.Id == CategoryId); //dbCotext.Catrgories.Find(CategoryId);
            return View(category);
        }
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            ModelState.Remove("Movies");

            if (ModelState.IsValid)
            {
                _categoryRepository.Update(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);

        }
        public IActionResult Delete(int CategoryId)
        {
            var category = _categoryRepository.GetOne(filter: e => e.Id == CategoryId) as Category;
            if (category != null)
            {
                _categoryRepository.Delete(category);
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
