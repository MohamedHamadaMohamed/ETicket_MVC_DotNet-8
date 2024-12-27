namespace ETicket.Data.Acess.layer.Models
{
    public class Actor
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;
		
		
        public string Bio { get; set; } = string.Empty;
        public string ProfilePicture { get; set; } = string.Empty;
        public string News { get; set; } = string.Empty;
        public List<Movie> Movies { get; set; } = null!;

        public List<ActorMovie> ActorMovies { get; set; } = null!;
    }
}
