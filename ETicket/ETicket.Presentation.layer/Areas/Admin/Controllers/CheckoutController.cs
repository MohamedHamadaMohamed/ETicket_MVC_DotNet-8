//using ETicket.Utility.IUtilities;
using ETicket.Business.Logic.layer.Repository.IRepository;
using ETicket.Data.Acess.layer.Models;
using ETicket.Utility.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace ETicket.Presentation.layer.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class CheckoutController : Controller
    {
        private readonly IEmailSender _emailSender;
		private readonly IMovieRepository _movieRepository;
        private readonly IUnitOfWork _unitOfWork;

		public CheckoutController(IEmailSender emailSender, IUnitOfWork unitOfWork)
        {
            _emailSender = emailSender;
            _unitOfWork = unitOfWork;
        }
        public IActionResult Cancel()
        {
            return View();
        }
        public IActionResult Success()
        {
            string message = "payment is successful , yor booked 3 ticket in {Venom: Let There Be Carnage} movie \n\n" +
				"in {Photospace} Cinema\r\n " +
				"for { Cartoon} Category\r\n" +
                "in 01/04/2023\r\n" +
                "your Seats : 5 , 6 , 7\r\n" +
                "your barCode : 1579335\r\n" +
                "Please , not share this code with any one ";

			_emailSender.SendEmailAsync("mohamedhamadamohamed71.7@gmail.com",
                "successful Payment", message);
            return View();
        }
    }
}
