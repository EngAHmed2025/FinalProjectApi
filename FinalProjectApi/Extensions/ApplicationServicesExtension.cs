using FinalProject.Core.Repositories.Contract;
using FinalProject.Repository;
using FinalProjectApi.Errors;
using FinalProjectApi.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectApi.Extensions
{
    public static class  ApplicationServicesExtension
    {
        public  static IServiceCollection AddApplicationServices( this IServiceCollection services)

        {
           services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
           services.AddAutoMapper(typeof(MappingProfiles));
           services.Configure<ApiBehaviorOptions>(
                options =>
                {
                    options.InvalidModelStateResponseFactory = (ActionContext) =>

                    {
                        var errors = ActionContext.ModelState.Where(P => P.Value.Errors.Count() > 0)
                        .SelectMany(P => P.Value.Errors)
                        .Select(E => E.ErrorMessage)
                        .ToList();

                        var response = new ApiValidationErrorResponse()
                        {

                            Errors = errors
                        };

                        return new BadRequestObjectResult(response);

                    };


                });

            services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));

            return services;
        }

        public static  WebApplication UseSwaggerMiddleWare( this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        } 
    }
}
