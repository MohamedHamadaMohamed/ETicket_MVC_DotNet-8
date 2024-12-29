using ETicket.Business.Logic.layer.Repository.IRepository;
using ETicket.Data.Acess.layer.Models;
using Microsoft.AspNetCore.Mvc;

namespace ETicket.Presentation.layer.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class RequestCinemaController : Controller
    {
        private IRequestCinemaRepository _requestCinemaRepository;
        private ICinemaRepository _cinemaRepository;

        public RequestCinemaController(IRequestCinemaRepository requestCinemaRepository, ICinemaRepository cinemaRepository)
        {
            this._requestCinemaRepository = requestCinemaRepository;
            this._cinemaRepository = cinemaRepository;
        }
        public IActionResult Index()
        {
            List<RequestCinema> requestCinema = _requestCinemaRepository.Get().ToList();
            return View(requestCinema);
        }
        public IActionResult CreateNewRequest()
        {
            return View(new RequestCinema());
        }
        [HttpPost]
        public async Task<IActionResult> CreateNewRequest(RequestCinema requestCinema)
        {

           await _requestCinemaRepository.CreateAsync(requestCinema);
            TempData["Success"] = "The request has been added successfully, please wait 3 days";
            return RedirectToAction(nameof(Index), "Home",new {area="Home"});
        }

        public async Task<IActionResult> Accept(int RequestId)
        {
            var request = _requestCinemaRepository.GetOne(filter: e => e.Id == RequestId) as RequestCinema;
            await _cinemaRepository.CreateAsync(new Cinema() { Name = request.Name, Description = request.Description, Address = request.Address, CinemaLogo = request.CinemaLogo });
            _requestCinemaRepository.Delete(request);

            return RedirectToAction(nameof(Index));

        }
        public IActionResult Reject(int RequestId)
        {
            var request = _requestCinemaRepository.GetOne(filter: e => e.Id == RequestId) as RequestCinema;
            _requestCinemaRepository.Delete(request);
            return RedirectToAction(nameof(Index));
        }
    }
}
