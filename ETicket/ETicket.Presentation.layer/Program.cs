using ETicket.Business.Logic.layer.Repository.IRepository;
using ETicket.Business.Logic.layer.Repository;
using ETicket.Data.Acess.layer.Data;
using ETicket.Data.Acess.layer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Stripe;
using ETicket.Utility.Utilities;

namespace ETicket.Presentation.layer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<ApplicationDbContext>
   (options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnention")));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            builder.Services.AddScoped<ICinemaRepository, CinemaRepository>();
            builder.Services.AddScoped<IMovieRepository, MovieRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IActorRepository, ActorRepository>();
            builder.Services.AddScoped<IActorMovieRepository, ActorMovieRepository>();
            builder.Services.AddScoped<IRequestCinemaRepository, RequestCinemaRepository>();

            builder.Services.AddScoped<IRequestCinemaRepository, RequestCinemaRepository>();

            // builder.Services.AddTransient<IEmailSender, EmailSender>();
           // builder.Services.AddScoped<IEmailSender, EmailSender>();




            builder.Services.AddScoped<ICartRepository, CartRepository>();

            builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));
            StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{area=Home}/{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
