using ETicket.Business.Logic.layer.Repository.IRepository;
using ETicket.Data.Acess.layer.Data;
using ETicket.Data.Acess.layer.Models;

namespace ETicket.Business.Logic.layer.Repository
{
    public class ActorRepository : Repository<Actor>, IActorRepository
    {
        public ActorRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
