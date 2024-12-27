using System.ComponentModel.DataAnnotations.Schema;

namespace ETicket.Data.Acess.layer.Models
{
    public class Cart
    {
        public string ApplicationUserId { get; set; } = null!;
        public ApplicationUser ApplicationUser { get; set; } = null!;


        public int MovieId { get; set; }
        public Movie Movie { get; set; } = null!;

        public int Count { set; get; }
    }
}
