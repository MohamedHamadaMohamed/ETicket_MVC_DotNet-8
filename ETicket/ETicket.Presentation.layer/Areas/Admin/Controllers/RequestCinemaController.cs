using ETicket.Business.Logic.layer.Repository.IRepository;
using ETicket.Data.Acess.layer.Models;
using Microsoft.AspNetCore.Mvc;

namespace ETicket.Presentation.layer.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class RequestCinemaController : Controller
    {
        private IUnitOfWork _unitOfWork;
        public RequestCinemaController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<RequestCinema> requestCinema = _unitOfWork.requestCinemaRepository.Get().ToList();
            return View(requestCinema);
        }
        public IActionResult CreateNewRequest()
        {
            return View(new RequestCinema());
        }
        [HttpPost]
        public async Task<IActionResult> CreateNewRequest(RequestCinema requestCinema)
        {

           await _unitOfWork.requestCinemaRepository.CreateAsync(requestCinema);
            TempData["Success"] = "The request has been added successfully, please wait 3 days";
            return RedirectToAction(nameof(Index), "Home",new {area="Home"});
        }

        public async Task<IActionResult> Accept(int RequestId)
        {
            var request = _unitOfWork.requestCinemaRepository.GetOne(filter: e => e.Id == RequestId) as RequestCinema;
            await _unitOfWork.cinemaRepository.CreateAsync(new Cinema() { Name = request.Name, Description = request.Description, Address = request.Address, CinemaLogo = request.CinemaLogo });
            _unitOfWork.requestCinemaRepository.Delete(request);

            return RedirectToAction(nameof(Index));

        }
        public IActionResult Reject(int RequestId)
        {
            var request = _unitOfWork.requestCinemaRepository.GetOne(filter: e => e.Id == RequestId) as RequestCinema;
            _unitOfWork.requestCinemaRepository.Delete(request);
            return RedirectToAction(nameof(Index));
        }
    }
}
