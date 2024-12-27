using ETicket.Data.Acess.layer.Models;

namespace ETicket.Presentation.layer.Areas.Admin.Models.ViewModels
{
    public class CartWithTotalPriceVM
    {

        public List<Cart> Carts { get; set; }
        public double TotalPrice { get; set; }
    }
}
