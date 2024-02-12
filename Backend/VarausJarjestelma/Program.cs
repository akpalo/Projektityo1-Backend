using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using VarausJarjestelma.Middleware;
using VarausJarjestelma.Models;
using VarausJarjestelma.Repositories;
using VarausJarjestelma.Services;
using System.Reflection;
using Microsoft.OpenApi.Models;
using ReservationSystem2022.Middleware;

namespace VarausJarjestelma
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers(); // Add services to the container.
            builder.Services.AddAuthentication("BasicAuthentication").AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);
            builder.Services.AddEndpointsApiExplorer();





            

            // Add services to the container.

            builder.Services.AddDbContext<ReservationContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("ReservationDB")));
            
            builder.Services.AddScoped<IItemService, ItemService>();
            builder.Services.AddScoped<IItemRepository, ItemRepository>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IReservationService, ReservationService>();
            builder.Services.AddScoped<IReservationrepository, ReservationRepository>();
            builder.Services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            //swagger
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Varausjärjestelmä API"

                });

            });

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod();
                    });
                    
            });



            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                ReservationContext dbcontext = scope.ServiceProvider.GetRequiredService<ReservationContext>();
                dbcontext.Database.EnsureCreated();

            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors();
            app.UseHttpsRedirection();
            app.UseMiddleware<ApikeyMiddleware>();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }

    }
}
