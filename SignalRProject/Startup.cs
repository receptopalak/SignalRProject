using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SignalRProject.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRProject
{
    public class Startup // SignalR  kullanýlabilmes için ve Hub a baðlanabilmesi için startup class ýna ihtiyacý vardýr.
    {
        public Startup(IConfiguration configuration) 
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) // Socket configuration burada yapýlýr. 
        {
            services.AddControllersWithViews();
            services.AddCors(options => options.AddDefaultPolicy(policy=> 
                                    policy.AllowAnyMethod()
                                        .AllowAnyHeader()
                                        .AllowCredentials()
                                        .SetIsOriginAllowed(origin => true)          // cors ayarlarý AddCors metodu ile yapýlýr. 
            ));
            services.AddSignalR(); // SignalR sisteme eklenir. 

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseCors();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<MyHub>("/myhub"); // SignalR ile socket oluþturabilmesi için bir end point oluþturulur. 
            });
        }
    }
}
