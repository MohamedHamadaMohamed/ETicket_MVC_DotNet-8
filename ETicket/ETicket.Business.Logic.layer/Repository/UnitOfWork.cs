using ETicket.Business.Logic.layer.Repository.IRepository;
using ETicket.Data.Acess.layer.Data;

namespace ETicket.Business.Logic.layer.Repository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _dbContext;
        public IActorMovieRepository actorMovieRepository { get; set; }
        public IActorRepository actorRepository { get; set; }
        public ICartRepository cartRepository { get; set; }
        public ICategoryRepository categoryRepository { get; set; }
        public ICinemaRepository cinemaRepository { get; set; }
        public IMovieRepository movieRepository { get; set; }
        public IRequestCinemaRepository requestCinemaRepository { get; set; }

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            actorMovieRepository = new ActorMovieRepository(dbContext);
            actorRepository = new ActorRepository(dbContext);
            cartRepository = new CartRepository(dbContext);
            categoryRepository = new CategoryRepository(dbContext);
            cinemaRepository = new CinemaRepository(dbContext);
            movieRepository = new MovieRepository(dbContext);
            requestCinemaRepository = new RequestCinemaRepository(dbContext);
        }
        public int Commit()
        {
           return _dbContext.SaveChanges();
        }
        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
