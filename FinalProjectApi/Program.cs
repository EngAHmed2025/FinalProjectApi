
using FinalProject.Core.Repositories.Contract;
using FinalProject.Repository;
using FinalProject.Repository.Data;
using FinalProjectApi.Errors;
using FinalProjectApi.Extensions;
using FinalProjectApi.Helpers;
using FinalProjectApi.MiddleWare;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

            //ApplicationServicesExtension.AddApplicationServices(builder.Services);

            builder.Services.AddApplicationServices();
                        
            var app = builder.Build();
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var _dbcontext = services.GetRequiredService<StoreContext>();
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();

            try
            {
                await _dbcontext.Database.MigrateAsync();
                await StoreContextSeed.SeedAsync(_dbcontext);
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
