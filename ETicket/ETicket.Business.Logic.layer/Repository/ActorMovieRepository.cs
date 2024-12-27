using ETicket.Business.Logic.layer.Repository.IRepository;
using ETicket.Data.Acess.layer.Data;
using ETicket.Data.Acess.layer.Models;

namespace ETicket.Business.Logic.layer.Repository
{
    public class ActorMovieRepository : Repository<ActorMovie>, IActorMovieRepository
    {
        public ActorMovieRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
