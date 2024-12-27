using Microsoft.AspNetCore.Mvc;

namespace ETicket.Presentation.layer.Areas.Customer.Controllers
{
    [Area(nameof(Customer))]
    public class MovieController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
