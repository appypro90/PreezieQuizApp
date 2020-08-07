using System.Reflection;
using AutoMapper;
using Db;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Services;
using Services.Mappers;
using Services.Validators;

namespace PreezieQuizApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(StartupAutoMapper).GetTypeInfo().Assembly);
            services.AddCors();
            services.AddControllers(options =>
                {
                    //options.Filters.Add(typeof(ModelStateValidator));
                })
                .AddFluentValidation(x =>
                {
                    //This line finds any public, non-abstract types that inherit from AbstractValidator and register them with the container
                    x.RegisterValidatorsFromAssemblyContaining<StartupValidator>();
                    x.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                });
            services.AddScoped<IQuizService, QuizService>();
            services.AddSingleton<IDataStore, DataStore>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(x => x
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
