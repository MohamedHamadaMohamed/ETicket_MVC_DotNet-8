using ETicket.Business.Logic.layer.Repository.IRepository;
using ETicket.Data.Acess.layer.Data;
using ETicket.Data.Acess.layer.Models;

namespace ETicket.Business.Logic.layer.Repository
{
    public class CinemaRepository : Repository<Cinema>, ICinemaRepository
    {
        public CinemaRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
