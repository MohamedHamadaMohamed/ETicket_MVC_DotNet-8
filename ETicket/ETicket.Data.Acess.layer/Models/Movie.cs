using ETicket.Data.Acess.layer.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace ETicket.Data.Acess.layer.Models
{
    public class Movie
    {
        public int Id { get; set; }
        
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        
        public double Price { get; set; }
        public string ImgUrl { get; set; } = string.Empty;
        public string? TrailerUrl { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public MovieStatus MovieStatus { get; set; }
        public int CinemaId { get; set; }
        public int CategoryId { get; set; }

        public Cinema Cinema { get; set; } = null!;

        public int views { set; get; }

        public Category Category { get; set; } = null!;


        public List<Actor> Actors { get; set; } = null!;
        public List<ActorMovie> ActorMovies { get; set; } = null!;

    }
}
