using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DAL.Context
{
    //public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    //{
    //    public AppDbContext CreateDbContext(string[] args)
    //    {
    //        var basePath = Path.Combine(Directory.GetCurrentDirectory(), "../Presentation");

    //        var configuration = new ConfigurationBuilder()
    //            .SetBasePath(basePath)
    //            .AddJsonFile("appsettings.json")
    //            .Build();

    //        var connectionString = configuration.GetConnectionString("DefaultConnection");

    //        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
    //        optionsBuilder.UseSqlServer(connectionString);

    //        return new AppDbContext(optionsBuilder.Options);
    //    }
    //}
}
