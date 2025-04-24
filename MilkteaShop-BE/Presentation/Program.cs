using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Presentation.ResolveDependencies;
using System.Net;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        builder.Services.AddControllers();
        builder.Configuration.AddEnvironmentVariables();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "MilkTea Shop API",
                Version = "v1",
                Description = "API for MilkTea Shop application"
            });
        });

        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        builder.Services.ResolveServices(connectionString);

        var app = builder.Build();

        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                // Log error details here
                await context.Response.WriteAsync(new
                {
                    context.Response.StatusCode,
                    Message = "Internal Server Error."
                }.ToString());
            });
        });


        ConfigurePipeline(app);

        app.Run();

        void ConfigurePipeline(WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            // Global middleware
            app.UseCors("AllowAll");
            app.UseHttpsRedirection();
            app.UseAuthorization();

            // Endpoint routing
            app.MapControllers();


        }
    }
}