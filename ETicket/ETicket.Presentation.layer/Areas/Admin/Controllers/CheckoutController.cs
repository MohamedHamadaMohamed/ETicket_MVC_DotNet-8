//using ETicket.Utility.IUtilities;
using Microsoft.AspNetCore.Mvc;

namespace ETicket.Presentation.layer.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class CheckoutController : Controller
    {
        /*private readonly IEmailSender _emailSender;
        public CheckoutController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }*/
        public IActionResult Cancel()
        {
            return View();
        }
        public IActionResult Success()
        {
            return View();
        }
    }
}
