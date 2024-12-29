using ETicket.Data.Acess.layer.Models;

namespace ETicket.Business.Logic.layer.Repository.IRepository
{
    public interface IUnitOfWork 
    {
        IActorMovieRepository actorMovieRepository { get; set; }
        IActorRepository actorRepository { get; set; }

        ICartRepository cartRepository {  get; set; }
        ICategoryRepository categoryRepository { get; set; }
        ICinemaRepository cinemaRepository { get; set; }
        IMovieRepository movieRepository { get; set; }
        IRequestCinemaRepository requestCinemaRepository { get; set; }


    }
}
