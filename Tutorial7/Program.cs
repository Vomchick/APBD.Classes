using Tutorial7.Dependencies;
using Tutorial7.Middleware;

namespace Tutorial7
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services
                .AddEndpointsApiExplorer()
                .AddSwaggerGen()
                .AddApplicationServices()
                .AddProblemDetails();

            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

            var app = builder.Build();

            // Use registered global error handler
            app.UseExceptionHandler();
            app.UseStatusCodePages();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
