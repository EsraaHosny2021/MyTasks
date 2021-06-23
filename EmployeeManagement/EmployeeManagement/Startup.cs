using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement
{
    public class Startup
    {
        private IConfiguration _config;
        //constructor
        public Startup(IConfiguration config)
        {
            _config = config;
        }
       

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //UseSqlServer : acts as database provider
            //IConfiguration: is service provided by asp.net core
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(_config.GetConnectionString("EmployeeDBConnection")));
            services.AddMvc().AddXmlSerializerFormatters();
            services.AddMvc(options => options.EnableEndpointRouting = false);
            //services.AddTransient<IEmployeeRepository, MockEmployeeRepository>();
            services.AddScoped<IEmployeeRepository, SQLEmployeeRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
            //UseMvc: doesn't support any default route
            app.UseMvc(routes => {
                routes.MapRoute("default","Company/{controller=Home}/{action=Index}/{id?}");
            });
            //app.UseMvc();
            app.UseRouting();

            app.Run(async (context) => {
                await context.Response.WriteAsync("Hello World!");
                
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    //endpoints.MapControllers();
                    await context.Response.WriteAsync("Hello World!");
                   
                   /* endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                    endpoints.MapRazorPages();*/
                });
            });

            
        }
    }
}
