using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Cerco.Animals.Data;
using FluentValidation.AspNetCore;
using Cerco.Animals.Data.Repositories.Interfaces;
using Cerco.Animals.Data.Repositories;

namespace Cerco.Animals.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddFluentValidation(fvc => fvc.RegisterValidatorsFromAssemblyContaining<Startup>())
                .AddViewLocalization()
                .AddDataAnnotationsLocalization();

            // Register in-memory database
            services.AddDbContext<AnimalContext>(
                options => options.UseInMemoryDatabase("Animaldb")
            );

            //services.Add(new ServiceDescriptor(typeof(CompanyService), this));
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IAnimalRepository, AnimalRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}