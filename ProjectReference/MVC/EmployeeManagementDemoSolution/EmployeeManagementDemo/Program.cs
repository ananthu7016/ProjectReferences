using EmployeeManagementDemo.Repository;

namespace EmployeeManagementDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();


            //-------------------------------------------
            // step one - Add a connection string 
            // - ie we need to go to appsetting.Developement.Json


            //-------------------------------------------
            //step Two - Register the connection string 
                         // we need to copy the connection string name from the Appsetting.Dev.Json and
            var connectionString = builder.Configuration.GetConnectionString("ConnectionStringMVC");

            // we need to Register repository and service as Middleware
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            /* What is Middleware in ASP.NET MVC
             
             Middleware in ASP.NET core MVC is software component that are used to handle request and response as they flow through the application pipeline
            
            Each middleware component in the pipeline can inspect, modify,or short-circuit the request or response as needed 

            MiddleWare provides a flexible way to add cross cutting concers such as loggings , authendication , authorization, compression , and more to an 
            Asp.Net Core application 

            Why we use middleware in ASP.NEt core MVC 
            Middleware allows developers to encapsulate common functionalities and apply it uniformly across the application 

             */
           

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Employees}/{action=Index}/{id?}");

            app.Run();
        }
    }
}