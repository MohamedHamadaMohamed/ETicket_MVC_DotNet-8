namespace ETicket.Data.Acess.layer.Models
{
    public class ActorMovie
    {
        public int ActorsId { get; set; }
        public int MoviesId { get; set; }

        public Movie Movies { get; set; }
        public Actor Actors { get; set; }
    }
}
