using ClimbingApp.Contracts.Repositories;
using ClimbingApp.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ClimbingApp
{
    public class Startup
    {
        public IConfiguration configRoot { get; }
        public Startup(IConfiguration configuration) { configRoot = configuration; }

        public void ConfigureServices(ConfigurationManager config, IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });
            services.AddScoped<IDatabaseRepository, DatabaseRepository>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                                .GetBytes(config.GetSection("AppSettings:Token").Value)),
                            ValidateIssuer = false,
                            ValidateAudience = false
                        };
                    });
            //builder.Services.AddAutoMapper(typeof(Program));
            services.AddMvcCore();


            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder => 
                        builder.AllowAnyHeader()
                        .AllowAnyMethod()
                        .SetIsOriginAllowed(origin => true) // allow any origin
                        .AllowCredentials()
                        //.WithOrigins("http://localhost:3000", "http://127.0.0.1:3000")
                        );
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseCors("AllowAll");
            app.UseEndpoints(builder =>
            {
                builder.MapControllers().RequireCors("AllowAll");
            });

            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

        }

    }
}
