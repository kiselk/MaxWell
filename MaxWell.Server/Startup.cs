using System;
using System.IO;
using MaxWell.Server.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MaxWell.Server
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
         

            //services.AddMvc().AddJsonOptions(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            //
            services.AddMvc().AddJsonOptions(x => x.SerializerSettings.ReferenceLoopHandling
                = Newtonsoft.Json.ReferenceLoopHandling.Serialize).AddJsonOptions(x => x.SerializerSettings.PreserveReferencesHandling
                = Newtonsoft.Json.PreserveReferencesHandling.Objects); ;
            services.AddLogging(lb =>
            {
                lb.AddConfiguration(Configuration.GetSection("Logging"));
             //   lb.AddFile(o => o.RootPath = AppContext.BaseDirectory);
            });
            string reg = "Server=tcp:maxwell.vmobile.online,1433;Database=u0620868_maxwellsql;User ID=u0620868_maxwellsqladmin;Password=maxwellsqlPass1!;Encrypt=false;Connection Timeout=30;";
            //if(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
            /*string azure = Configuration.GetConnectionString("MyDbConnection");
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                    {
                        options.ExpireTimeSpan = TimeSpan.FromDays(7);
                        options.LoginPath = "/Account/CustomLogin";
                        options.Cookie.Name = "MaxWellCookieName";
                    }
                );*/
            services.AddDbContext<MyDatabaseContext>(options => options.UseSqlServer(reg).EnableSensitiveDataLogging());
         //   services.AddDbContext<MyDatabaseContext>(options =>
        //        options.UseSqlServer(cs));
           /*
             else

                services.AddDbContext<MyDatabaseContext>(options =>
                        options.UseSqlite("Data Source=localdatabase.db"));
                        */
 
         services.BuildServiceProvider().GetService<MyDatabaseContext>().Database.Migrate();
        //    services.BuildServiceProvider().GetService<AllergenDatabaseContext>().Database.Migrate();


            //   services.AddSingleton<IToDoRepository,ToDoRepository>();
            //   services.AddSingleton<IPersonRepository, PersonRepository>();
            //    services.AddScoped<SignInManager<IdentityUser>, SignInManager<IdentityUser>>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseMvc(routes => { routes.MapRoute(name: "default", template: "{controller=Home}/{action=Index}/{id?}"); });
        /*    if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/VMobile/Error");
            }

            app.UseStaticFiles();

            app.UseOoui();

            Xamarin.Forms.Forms.Init();
     app.UseMvc(routes => { routes.MapRoute(name: "default", template: "{controller=VMobile}/{action=Index}/{id?}"); });
       */
        }
    }
}
