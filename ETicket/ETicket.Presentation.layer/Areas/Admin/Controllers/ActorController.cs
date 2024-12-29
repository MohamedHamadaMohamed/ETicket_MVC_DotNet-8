using ETicket.Business.Logic.layer.Repository.IRepository;
using ETicket.Data.Acess.layer.Models;
using ETicket.Presentation.layer.Areas.Admin.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ETicket.Presentation.layer.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class ActorController : Controller
    {
       // private IActorRepository _actorRepository;
        private IUnitOfWork _unitOfWork;
        public ActorController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public IActionResult Index(string? query = null, int PageNumber = 1)
        {
            ActorsVM actorsVM = new ActorsVM();
            var actors = _unitOfWork.actorRepository.Get();
            if(query != null)
            {
                query = query.Trim();

                actors = actors.Where(e=>e.News.Contains(query)||e.FirstName.Contains(query)||e.LastName.Contains(query)||e.Bio.Contains(query));
            }
            actorsVM.TotalActorCount = (actors.Count()+4)/5;
            if(PageNumber<1) PageNumber = 1;
            actors = actors.Skip((PageNumber - 1) * 5).Take(5);
            actorsVM.CurrentPageIndex = PageNumber;
            actorsVM.actors = actors.ToList();

            return View(actorsVM);
        }
        public IActionResult Details(int ActorId)
        {
            var actor = _unitOfWork.actorRepository.GetOne(filter: e => e.Id == ActorId) as Actor;
            return View(actor);
        }
        public IActionResult Create()
        {
            return View(new Actor());
        }
        [HttpPost]
        public async Task<IActionResult> Create(Actor actor, IFormFile file)
        {
            ModelState.Remove("ProfilePicture");
            ModelState.Remove("Movies");
            ModelState.Remove("ActorMovies");

            if (ModelState.IsValid)
            {
                if (file != null && file.Length > 0)
                {
                   
                    actor.ProfilePicture = Utility.Utilities.Utility.UploadFile(file, "cast"); ;
                }

               await _unitOfWork.actorRepository.CreateAsync(actor);
                return RedirectToAction(nameof(Index));
            }
            return View(actor);
        }
        public IActionResult Edit(int ActorId)
        {
            var actor = _unitOfWork.actorRepository.GetOne(filter: e => e.Id == ActorId) as Actor;
            return View(actor);
        }
        [HttpPost]
        public IActionResult Edit(Actor actor, IFormFile file)
        {
            ModelState.Remove("ProfilePicture");
            ModelState.Remove("Movies");
            ModelState.Remove("ActorMovies");
            if (ModelState.IsValid)
            {
                var oldActor = _unitOfWork.actorRepository.GetOne(filter: e => e.Id == actor.Id, trancked: false);
                if (file != null && file.Length > 0)
                {
                    actor.ProfilePicture = Utility.Utilities.Utility.UploadFile(file, "cast"); ;
                    Utility.Utilities.Utility.DeleteFile(oldActor.ProfilePicture, "cast");

                }
                else
                {
                    actor.ProfilePicture = oldActor.ProfilePicture;
                }


                _unitOfWork.actorRepository.Update(actor);
                return RedirectToAction(nameof(Index));
            }
            return View(actor);
        }

        public IActionResult Delete(int ActorId)
        {
            var actor = _unitOfWork.actorRepository.GetOne(filter: e => e.Id == ActorId);

            Utility.Utilities.Utility.DeleteFile(actor.ProfilePicture, "cast");



            if (actor != null)
            {
                _unitOfWork.actorRepository.Delete(actor);
            }
            return RedirectToAction(nameof(Index));

        }
    }
}
