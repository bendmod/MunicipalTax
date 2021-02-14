using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MunicipalTax.Data;
using MunicipalTax.Data.MapperProfiles;
using MunicipalTax.Data.Repositories;
using MunicipalTax.Logic.Facades;
using MunicipalTax.Logic.Interfaces.Facades;
using MunicipalTax.Logic.Interfaces.Repositories;
using MunicipalTax.Logic.Interfaces.Services;
using MunicipalTax.Logic.Interfaces.Validators;
using MunicipalTax.Logic.Mapping;
using MunicipalTax.Logic.Services;
using MunicipalTax.Logic.Validators;

namespace MunicipalTax
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddJsonOptions(opts =>
            {
                opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<LogicProfile>();
                cfg.AddProfile<RepositoryProfile>();
            }, typeof(Startup));


            AddContainerServices(services);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Municipality Tax API", Version = "v1" });
            });
        }

        private void AddContainerServices(IServiceCollection services)
        {
            services.AddDbContext<DataBaseContext>();
            services.AddTransient<IFileFacade, FileFacade>();
            services.AddTransient<ITaxFacade, TaxFacade>();

            services.AddTransient <ITaxService, TaxService> ();
            services.AddTransient<IFileService, FileService>();

            services.AddTransient <IMunicipalityValidator, MunicipalityValidator> ();
            services.AddTransient <IDateValidator, DateValidator> ();
            
            services.AddTransient<IMunicipalityRepository, MunicipalityRepository>();
            services.AddTransient<ITaxRepository, TaxRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("./v1/swagger.json", "Municipality Tax API v1");
            });
        }
    }
}
