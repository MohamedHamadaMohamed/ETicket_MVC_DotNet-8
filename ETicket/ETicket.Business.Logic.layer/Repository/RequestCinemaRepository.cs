using ETicket.Business.Logic.layer.Repository.IRepository;
using ETicket.Data.Acess.layer.Data;
using ETicket.Data.Acess.layer.Models;

namespace ETicket.Business.Logic.layer.Repository
{
    public class RequestCinemaRepository : Repository<RequestCinema>, IRequestCinemaRepository
    {
        public RequestCinemaRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
