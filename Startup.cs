using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using PaylocityDemo.Data;
using PaylocityDemo.Entity;
using PaylocityDemo.Manager;
using PaylocityDemo.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace PaylocityDemo
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
            services.AddControllers();

            // create automapper mappings (used to map DTOs from DB to API result objects)
            AutoMappings.Current.RegisterMapping(Assembly.GetExecutingAssembly().GetExportedTypes());

            // Add DB context using conn string from env var
            services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(Environment.GetEnvironmentVariable("PAYLOCITY_DEMO_DB")));

            // Add repositories
            services.AddScoped<IRepository<Benefit>, Repository<Benefit>>();
            services.AddScoped<IRepository<BenefitDiscount>, Repository<BenefitDiscount>>();
            services.AddScoped<IRepository<Employee>, Repository<Employee>>();
            services.AddScoped<IRepository<Payroll>, Repository<Payroll>>();

            // Add managers
            services.AddScoped<IBenefitManager, BenefitManager>();
            services.AddScoped<IBenefitDiscountManager, BenefitDiscountManager>();
            services.AddScoped<IEmployeeManager, EmployeeManager>();
            services.AddScoped<IPayrollManager, PayrollManager>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Paylocity Demo API",
                    Version = "v1",
                    Description = "Demo API for coding challenge",
                    Contact = new OpenApiContact
                    {
                        Name = "Sebastian Stefaniuk",
                        Email = "sebboness@gmail.com",
                    },
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable swagger UI
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Zomato API V1");

                // To serve SwaggerUI at application's root page, set the RoutePrefix property to an empty string.
                c.RoutePrefix = string.Empty;
            });

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
