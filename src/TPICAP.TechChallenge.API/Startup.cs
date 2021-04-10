using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using TPICAP.TechChallenge.API.Middleware;
using TPICAP.TechChallenge.Data;
using TPICAP.TechChallenge.Data.Services;
using TPICAP.TechChallenge.Infrastructure.Mappers;
using TPICAP.TechChallenge.Infrastructure.Services;

namespace TPICAP.TechChallenge.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(setupAction =>
                {
                    setupAction.ReturnHttpNotAcceptable = true;
                })
                .ConfigureApiBehaviorOptions(setupAction =>
                {
                    
                    setupAction.InvalidModelStateResponseFactory = context =>
                    {
                        var problemDetailsFactory = context.HttpContext.RequestServices
                                .GetRequiredService<ProblemDetailsFactory>();
                        var problemDetails = problemDetailsFactory.CreateValidationProblemDetails(
                                context.HttpContext,
                                context.ModelState);

                        problemDetails.Detail = "See the errors field for details.";
                        problemDetails.Instance = context.HttpContext.Request.Path;


                        var actionExecutingContext =
                                  context as Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext;

                        if (context.ModelState.ErrorCount > 0)
                        {
                            problemDetails.Type = "https://Persons.com/modelvalidationproblem";
                            problemDetails.Status = StatusCodes.Status422UnprocessableEntity;
                            problemDetails.Title = "One or more validation errors occurred.";
                            
                            return new UnprocessableEntityObjectResult(problemDetails)
                            {
                                ContentTypes = { "application/problem+json" }
                            };
                        }

                        problemDetails.Status = StatusCodes.Status400BadRequest;
                        problemDetails.Title = "One or more errors on input occurred.";
                        return new BadRequestObjectResult(problemDetails)
                        {
                            ContentTypes = { "application/problem+json" }
                        };
                    };
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "TPICAP.TechChallenge.API", Version = "v1"});
            });

            services.AddLogging(builder =>
                builder
                    .AddDebug()
                    .AddConsole()
                    .AddConfiguration(Configuration.GetSection("Logging"))
                    .SetMinimumLevel(LogLevel.Information)
            );

            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<ISalutationRepository, SalutationRepository>();

            services.AddTransient<PersonCreationDtoToPersonEntityMapper>();
            services.AddTransient<PersonUpdateDtoToPersonEntityMapper>();
            services.AddTransient<PersonEntityToPersonDtoMapper>();
            services.AddTransient<IPersonService, PersonService>();

            services.AddTransient<IPropertyCheckerService, PropertyCheckerService>();
            services.AddTransient<IHateoasLinksCreator, HateoasLinksCreator>();

            services.AddDbContext<PeopleContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("PeopleDBConnection"));
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", ""));
            }

            app.UseRouting();

            app.UseMiddleware<RequestResponseLoggingMiddleware>();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            


        }
    }
}