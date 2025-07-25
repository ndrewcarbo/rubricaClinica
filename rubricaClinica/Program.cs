
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using rubricaClinica.Context;
using rubricaClinica.Repos;
using rubricaClinica.Services;

namespace rubricaClinica
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

            builder.Services.AddScoped<AdminRepos>();
            builder.Services.AddScoped<AdminService>();

            builder.Services.AddDbContext<RubricaClinicaContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<PazienteRepositories>();
            builder.Services.AddScoped<PazienteService>();

            builder.Services.AddScoped<AppuntamentoRepositories>();
            builder.Services.AddScoped<AppuntamentoService>();

            


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors(builder =>
            builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader()

            );
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
