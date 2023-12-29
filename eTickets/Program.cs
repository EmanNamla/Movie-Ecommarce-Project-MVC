using eTickets.BL.UnitOfWork;
using eTickets.DAL.Contexts;
using eTickets.DAL.Data.DataSeed;
using eTickets.PL.Helpers;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace eTickets
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //DbContext 
            builder.Services.AddDbContext<AppDbContext>(options=>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefultConnectionString"));
            });
            //Allow DJ To UnitofWork And AutoMapper
            builder.Services.AddScoped<IUnitofWork, UnitofWork>();
            builder.Services.AddAutoMapper(typeof(MappingProfiles));

            var app = builder.Build();


            ///Seeding
            using var scope=app.Services.CreateScope();
            var Services = scope.ServiceProvider;
            var dbContext = Services.GetRequiredService<AppDbContext>();
            await dbContext.Database.MigrateAsync();
            await AppContextSeed.Seed(dbContext);


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
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}