using System.Text.Json.Serialization;
using DemoEmployeeManagementRestApi.Model;
using DemoEmployeeManagementRestApi.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace DemoEmployeeManagementRestApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


         
            // JSON Format 
            builder.Services.AddControllersWithViews().AddJsonOptions(opt=>
            {
                opt.JsonSerializerOptions.PropertyNamingPolicy = null;
                opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                opt.JsonSerializerOptions.WriteIndented = true;
            });
         

            //we need to set the connection string in middleware

            builder.Services.AddDbContext<PropelDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("propelJanConnection")));


            // Repositoty as Middleware
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<ILoginRepository, LoginRepository>();


            builder.Services.AddCors(option =>
            {
                option.AddPolicy("AllowAllOrgin",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                    });
            });


               // we need to register the JWT authndication Scheema 
               builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
               {
                   opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                  {
                           ValidateIssuer = true,
                           ValidateAudience = true,
                           ValidateLifetime = true,
                           ValidateIssuerSigningKey = true,
                           ValidIssuer = builder.Configuration["Jwt:Issuer"],
                           ValidAudience = builder.Configuration["Jwt:Issuer"],
                           IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };

               });
            


            var app = builder.Build();
            //app.UseCors(option=>
            //             option.AllowAnyMethod()
            //             .AllowAnyHeader()
            //             .AllowCredentials());

            // Configure the HTTP request pipeline.

            app.UseCors("AllowAllOrgin");

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();
            

            app.MapControllers();

            app.Run();
        }
    }
}
