using System.ComponentModel.DataAnnotations;

namespace ETicket.Data.Acess.layer.Models
{
    public class RequestCinema
    {
        public int Id { get; set; }
       
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string CinemaLogo { get; set; } = null!;
        public string Address { get; set; } = null!;

        public string Details { get; set; } = null!;
    }
}
