
using FinalProject.Core.Models.Identity;
using FinalProject.Core.Repositories.Contract;
using FinalProject.Core.Services.Contract;
using FinalProject.Repository;
using FinalProject.Repository.Data;
using FinalProject.Repository.Data.Identity;
using FinalProject.Services;
using FinalProjectApi.Errors;
using FinalProjectApi.Extensions;
using FinalProjectApi.Helpers;
using FinalProjectApi.MiddleWare;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Text;

namespace FinalProjectApi
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<StoreContext>(
               
                options => { options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection"));
                });
            builder.Services.AddDbContext<AppIdentityDbContext>(

              options => {
                  options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
              });
            builder.Services.AddAuthentication().AddJwtBearer("Bearer", options => {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["JWT: ValidIssure"],
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWT:ValidAudience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:AuthKey"] ?? string.Empty))
                };
            });
            builder.Services.AddSingleton<IConnectionMultiplexer>((serviceProvider) =>
            {
                var connection = builder.Configuration.GetConnectionString("Redis");
                
                return ConnectionMultiplexer.Connect(connection);
            });

            //ApplicationServicesExtension.AddApplicationServices(builder.Services);

            builder.Services.AddApplicationServices();
            builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                //options.Password.RequiredUniqueChars = 2;
                //options.Password.RequireDigit = true;

            }).AddEntityFrameworkStores<AppIdentityDbContext>();
            builder.Services.AddScoped(typeof(IAuthService), typeof(AuthService));
            var app = builder.Build();
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var _dbcontext = services.GetRequiredService<StoreContext>();
            var _identityDbContext = services.GetRequiredService<AppIdentityDbContext>();
            var _userManager = services.GetRequiredService<UserManager<AppUser>>();
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
           
            try
            {
                await _dbcontext.Database.MigrateAsync();
                await StoreContextSeed.SeedAsync(_dbcontext);
                await _identityDbContext.Database.MigrateAsync();
                await AppIdentityDbContextSeed.SeedUserAsync(_userManager);
                
            }catch (Exception ex) 
            {

                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An Error occurred during Migration");
            }
            app.UseMiddleware<ExceptionMiddleware>();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddleWare(); 
            }

            app.UseStatusCodePagesWithReExecute("/Errors/{0}");

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseStaticFiles();
            app.MapControllers();

            app.Run();
        }
    }
}
