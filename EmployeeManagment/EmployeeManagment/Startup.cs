using EmployeeManagment.Models;
using EmployeeManagment.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagment
{
    public class Startup
    {
        private IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContextPool<AppDbContext>(options => options.UseSqlServer(_config.GetConnectionString("EmployeeDBConnection")));
            services.AddMvc(options => options.EnableEndpointRouting = false);
            services.AddMvc(options => {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            });
            //services.AddSingleton<IEmployeeRepository, MockEmployeeRepository>(); 

            services.AddIdentity<ApplicationUser, IdentityRole>(
                options =>
                {
                    options.Password.RequiredLength = 10;
                    options.Password.RequiredUniqueChars = 3;
                }
                ).AddEntityFrameworkStores<AppDbContext>();


            services.AddAuthorization(option =>
           {
               option.AddPolicy("DeleteRolePolicy", policy => policy.RequireClaim("Delete Role", "true"));
               //option.AddPolicy("EditRolePolicy", policy => policy.RequireClaim("Edit Role", "true"));

               //option.AddPolicy("EditRolePolicy", policy => policy.RequireAssertion(
               //    context => context.User.IsInRole("Admin") &&
               //    context.User.HasClaim(claim => claim.Type == "Edit Role" && claim.Value =="true" ) ||
               //    context.User.IsInRole("Super Admin")
               //    ));

               option.AddPolicy("EditRolePolicy", policy => policy.AddRequirements(new ManageAdminRolesAndClaimsRequirement()));
           });

            services.AddScoped<IEmployeeRepository, SQLEmployeeRepository>();
            services.AddSingleton<IAuthorizationHandler, CanEditOnlyOtherAdminRolesAndClaimsHandler>();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                //DeveloperExceptionPageOptions developerExceptionPageOptions = new DeveloperExceptionPageOptions
                //{
                //    SourceCodeLineCount = 0
                //};
                //app.UseDeveloperExceptionPage(developerExceptionPageOptions);

                app.UseDeveloperExceptionPage();
            }
            else
			{
                app.UseExceptionHandler("/Error");
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
            }
            app.UseRouting();


            // Middle Ware Code

            //app.Use(async (context, next) => {
            //    logger.LogInformation("MW1 : Incoming request");
            //    //await context.Response.WriteAsync("Hello from 1st Middleware!");
            //    await next();
            //    logger.LogInformation("MW1 : Outgoing request");
            //});

            //app.Use(async (context, next) => {
            //    logger.LogInformation("MW2 : Incoming request");
            //    //await context.Response.WriteAsync("Hello from 1st Middleware!");
            //    await next();
            //    logger.LogInformation("MW2 : Outgoing request");
            //});

            //app.Run(async (context) => {
            //    await context.Response.WriteAsync("MW3: Request handled and response produced");

            //    logger.LogInformation("MW3: Request handled and response produced");
            //});



            //FileServerOptions fileServerOptions = new FileServerOptions();
            //fileServerOptions.DefaultFilesOptions.DefaultFileNames.Clear();

            //fileServerOptions.DefaultFilesOptions.DefaultFileNames.Add("food.html");

            //app.UseFileServer(fileServerOptions);

            //DefaultFilesOptions defaultFilesOptions = new DefaultFilesOptions();
            //defaultFilesOptions.DefaultFileNames.Clear();
            //defaultFilesOptions.DefaultFileNames.Add("food.html");
            //app.UseDefaultFiles(defaultFilesOptions);
            //app.UseStaticFiles();

            app.UseStaticFiles();
            app.UseAuthentication();
            //app.UseMvcWithDefaultRoute();

            //app.UseMvc();

           

            app.UseMvc(routes =>
            {
                routes.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}");
            });

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hosting Envirnment :- " + env.EnvironmentName);
            //});

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World");
            //});



            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //        //await context.Response.WriteAsync(System.Diagnostics.Process.GetCurrentProcess().ProcessName);
            //        await context.Response.WriteAsync("Hello from 1st Middleware!");
            //    });
            //});

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //        //await context.Response.WriteAsync(System.Diagnostics.Process.GetCurrentProcess().ProcessName);
            //        await context.Response.WriteAsync("Hello from 2nd Middleware!");
            //    });
            //});


            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //        //await context.Response.WriteAsync(System.Diagnostics.Process.GetCurrentProcess().ProcessName);
            //        await context.Response.WriteAsync(_config["MyKey"]);
            //    });
            //});
        }
    }
}
