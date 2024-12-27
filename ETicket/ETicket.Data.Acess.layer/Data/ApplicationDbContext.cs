using ETicket.Data.Acess.layer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ETicket.Data.Acess.layer.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {

        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }

        public DbSet<ActorMovie> ActorMovies { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<RequestCinema> RequestCinemas { get; set; }

        public DbSet<Cart> Carts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Movie>()
                .HasKey(e => e.Id);
            modelBuilder.Entity<Cinema>()
                .HasKey(e => e.Id);
            modelBuilder.Entity<Category>()
                .HasKey(e => e.Id);
            modelBuilder.Entity<Actor>()
                .HasKey(e => e.Id);
            modelBuilder.Entity<ApplicationUser>()
               .HasKey(e => e.Id);
            modelBuilder.Entity<RequestCinema>()
               .HasKey(e => e.Id);
            modelBuilder.Entity<ActorMovie>()
                .HasKey(e => new { e.ActorsId, e.MoviesId });
            modelBuilder.Entity<Cart>()
                .HasKey(e => new {e.ApplicationUserId, e.MovieId});
            
            modelBuilder.Entity<Movie>()
                .Property(e => e.Name)
                .HasColumnType("varchar(100)");
            modelBuilder.Entity<Movie>()
                .Property(e => e.Description)
                .HasMaxLength(1000)
                .IsUnicode(false);
            modelBuilder.Entity<Movie>()
                .Property(e => e.TrailerUrl)
                 .HasMaxLength(1000);
            modelBuilder.Entity<Movie>()
                .Property(e => e.ImgUrl)
                .HasColumnType("varchar(1000)");


            modelBuilder.Entity<Cinema>()
                .Property(e => e.Name)
                .HasColumnType("varchar(1000)");
            modelBuilder.Entity<Cinema>()
               .Property(e => e.Description)
               .HasColumnType("varchar(1000)");
            modelBuilder.Entity<Cinema>()
               .Property(e => e.CinemaLogo)
               .HasColumnType("varchar(1000)");
            modelBuilder.Entity<Cinema>()
               .Property(e => e.Address)
               .HasColumnType("varchar(1000)");

            modelBuilder.Entity<Category>()
               .Property(e => e.Name)
               .HasColumnType("varchar(50)");

            modelBuilder.Entity<Actor>()
                .Property(e => e.FirstName)
                .HasColumnType("varchar(50)");
            modelBuilder.Entity<Actor>()
                .Property(e => e.LastName)
                .HasColumnType("varchar(50)");
            modelBuilder.Entity<Actor>()
                .Property(e => e.Bio)
                .HasColumnType("varchar(1000)");
            modelBuilder.Entity<Actor>()
                .Property(e => e.ProfilePicture)
                .HasColumnType("varchar(1000)");
            modelBuilder.Entity<Actor>()
                .Property(e => e.News)
                .HasColumnType("varchar(1000)");

            modelBuilder.Entity<Category>()
               .HasMany(e => e.Movies)
               .WithOne(e => e.Category)
               .HasForeignKey(e => e.CategoryId);
            modelBuilder.Entity<Movie>()
               .HasOne(e => e.Category)
               .WithMany(e => e.Movies)
               .HasForeignKey(e => e.CategoryId);

            modelBuilder.Entity<Cinema>()
               .HasMany(e => e.Movies)
               .WithOne(e => e.Cinema)
               .HasForeignKey(e => e.CinemaId);
            modelBuilder.Entity<Movie>()
               .HasOne(e => e.Cinema)
               .WithMany(e => e.Movies)
               .HasForeignKey(e => e.CinemaId);

            modelBuilder.Entity<Movie>()
                .HasMany(e => e.Actors)
                .WithMany(e => e.Movies);

            modelBuilder.Entity<Actor>()
                .HasMany(e => e.Movies)
                .WithMany(e => e.Actors);

            modelBuilder.Entity<ApplicationUser>()
                .Property(e => e.FirstName)
                .HasMaxLength(50);
            modelBuilder.Entity<ApplicationUser>()
                .Property(e => e.LastName)
                .HasMaxLength(50);
            

            modelBuilder.Entity<ApplicationUser>()
                .Property(e => e.Street)
                .HasMaxLength(50);
            modelBuilder.Entity<ApplicationUser>()
                .Property(e => e.City)
                .HasMaxLength(50);
            modelBuilder.Entity<ApplicationUser>()
                .Property(e => e.State)
                .HasMaxLength(50);
            modelBuilder.Entity<ApplicationUser>()
                .Property(e => e.Region)
                .HasMaxLength(50);

           


        }



    }
}
