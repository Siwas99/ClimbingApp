global using ClimbingApp.Data;
using ClimbingApp.Contracts.Repositories;
using ClimbingApp.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using ClimbingApp;

var builder = WebApplication.CreateBuilder(args); 
var startup = new Startup(builder.Configuration); 
startup.ConfigureServices(builder.Configuration, builder.Services); // calling ConfigureServices method
var app = builder.Build(); 
startup.Configure(app, builder.Environment); // calling Configure method
app.Run();

//namespace ClimbingApp;
//public static class Program
//{
//public static void Main(string[] args)
//{
//    Host.CreateDefaultBuilder(args).Build().Run();

//}

//public static IHostBuilder CreateHostBuilder(string[] args) =>
//   Host.CreateDefaultBuilder(args)
//       .ConfigureLogging((hostingContext, logging) =>
//       {
//           logging.ClearProviders();
//           logging.SetMinimumLevel(LogLevel.Trace);
//       })
//       .ConfigureWebHostDefaults(builder =>
//       {
//           builder.UseStartup<Startup>();
//       })
//       .UseWindowsService();

//}

// Add services to the container.

//builder.ConfigureServices((config, services) =>
//{
//    services.AddControllers();
//    services.AddDbContext<DataContext>(options =>
//    {
//        options.UseSqlServer(config.Configuration.GetConnectionString("DefaultConnection"));
//    });
//    services.AddScoped<IDatabaseRepository, DatabaseRepository>();

//    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//    services.AddEndpointsApiExplorer();
//    services.AddSwaggerGen();
//    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//            .AddJwtBearer(options =>
//            {
//                options.TokenValidationParameters = new TokenValidationParameters
//                {
//                    ValidateIssuerSigningKey = true,
//                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
//                        .GetBytes(config.Configuration.GetSection("AppSettings:Token").Value)),
//                    ValidateIssuer = false,
//                    ValidateAudience = false
//                };
//            });
//    //builder.Services.AddAutoMapper(typeof(Program));
//    services.AddMvcCore();


//    services.AddCors(options =>
//    {
//        options.AddPolicy(name: "AllowAll", builder => builder.WithOrigins("http://localhost:3000")
//                .AllowAnyHeader()
//                .AllowAnyMethod()
//                .AllowCredentials()
//                );
//    });

//});


//var app = builder.ConfigureAppConfiguration((appa, config) =>
//{


//config.UseRouting();

////app.UseAuthentication();

////app.UseAuthorization();

//app.UseCors("AllowAll");
//app.UseEndpoints(builder =>
//{
//    builder.MapControllers();
//});

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.MapControllers();

//app.Run();
//});
