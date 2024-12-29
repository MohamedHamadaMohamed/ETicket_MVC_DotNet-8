using ETicket.Business.Logic.layer.Repository.IRepository;
using ETicket.Data.Acess.layer.Models;
using ETicket.Presentation.layer.Areas.Admin.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;

namespace ETicket.Presentation.layer.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class CartController : Controller
    {
        ICartRepository _cartRepository;
        UserManager<ApplicationUser> _userManager;
        public CartController(ICartRepository cartRepository, UserManager<ApplicationUser> userManager)
        {
            this._cartRepository = cartRepository;
            this._userManager = userManager;
        }
        public IActionResult Index()
        {

            var carts = _cartRepository.Get(includeProps: [e => e.Movie]
                ,filter: e=>e.ApplicationUserId == _userManager.GetUserId(User)).ToList();
            CartWithTotalPriceVM cartWithTotalPriceVM = new CartWithTotalPriceVM()
            {
                Carts = carts.ToList(),
                TotalPrice = (double)carts.Sum(e => e.Count * e.Movie.Price)
            };
            return View(cartWithTotalPriceVM);
        }
        [HttpPost]
        public IActionResult AddToCart(int MovieId,int count=1)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);
                if (userId is null)
                {
                    return RedirectToAction("Login", "Account");

                }
                var CardTemp = _cartRepository.GetOne(filter: e => (e.MovieId == MovieId && e.ApplicationUserId == userId));
                if (CardTemp is null)
                {
                    _cartRepository.CreateAsync(new()
                    {
                        ApplicationUserId = userId,
                        MovieId = MovieId,
                        Count = count
                    });
                }
                else
                {
                    CardTemp.Count = CardTemp.Count+count;
                    _cartRepository.Update(CardTemp);
                    
                }

            }
            return RedirectToAction("Index","Home",new {area="Home"});
        }
        
        public IActionResult Pay()
        {
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
                SuccessUrl = $"{Request.Scheme}://{Request.Host}/Admin/Checkout/Success",
                CancelUrl = $"{Request.Scheme}://{Request.Host}/Admin/Checkout/Cancel",
            };

            var carts = _cartRepository.Get(includeProps: [e => e.Movie]
                , filter: e => e.ApplicationUserId == _userManager.GetUserId(User)).ToList();

            

			foreach (var  item in carts)
            {
                options.LineItems.Add(
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = item.Movie.Name,//model.ProductName,
                                Description = item.Movie.Description,
                            },
                            UnitAmount = (long)item.Movie.Price*100,
                        },
                        Quantity = item.Count,
                    });
            }
            var service = new SessionService();
            var session = service.Create(options);
            return Redirect(session.Url);
        }

       

        public IActionResult Increment(int MovieId)
        {
            var userId = _userManager.GetUserId(User);
            if(userId is null)
            {
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }
            var cart = _cartRepository.GetOne(filter:e=>((e.ApplicationUserId == userId)&& (e.MovieId == MovieId ) ));
            if (cart != null)
            {
                cart.Count++;
                _cartRepository.Update(cart);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Login", "Account", new { area = "Identity" });

        }
        public IActionResult decrement(int MovieId)
        {
            var userId = _userManager.GetUserId(User);
            if (userId is null)
            {
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }
            var cart = _cartRepository.GetOne(filter: e => e.MovieId == MovieId && e.ApplicationUserId == userId) as Cart;
            if (cart != null)
            {
                cart.Count--;
            if (cart.Count < 0) { cart.Count = 0; }
            _cartRepository.Update(cart);
            return RedirectToAction("Index");
            }
            return RedirectToAction("Login", "Account", new { area = "Identity" });

        }

        public  IActionResult Delete(int MovieId)
        {

            var userId = _userManager.GetUserId(User);
            if (userId is null)
            {
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }
            var cart = _cartRepository.GetOne(filter: e => e.MovieId == MovieId && e.ApplicationUserId == userId) as Cart;
            if (cart != null)
            {
                _cartRepository.Delete(cart);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Login", "Account", new { area = "Identity" });

        }
    }
}
