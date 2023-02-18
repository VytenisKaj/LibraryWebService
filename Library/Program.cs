using Infrastructure.Domains.Books.Repositories;
using Infrastructure.Domains.Books.Services;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace Library
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var server = builder.Configuration["Server"] ?? "localhost";
            var port = builder.Configuration["Port"] ?? "1433";
            var database = builder.Configuration["Database"] ?? "Library";

            // BAD PRACTICE, don't do this in real applications. This is only a demo application
            var user = builder.Configuration["User"] ?? "SA";
            var password = builder.Configuration["Password"] ?? "securePassw0rd";
            var connection = $"Server={server},{port};Initial Catalog={database}; User ID ={user};Password={password};TrustServerCertificate=True";

            builder.Services.AddDbContext<RepositoryContext>(options =>
                options.UseSqlServer(connection));

            ConfigureServices(builder.Services);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            DatabaseSetup.StartDatabase(app);

            app.Run();
        }

        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Library API", Version = "v1" });
            });

            services.AddAutoMapper(typeof(Program));
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IBookService, BookService>();
        }
    }
}

