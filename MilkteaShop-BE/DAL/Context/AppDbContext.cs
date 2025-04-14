using DAL.Models.Authentication;
using DAL.Models.Orders;
using DAL.Models.Products;
using DAL.Models.Promotions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DAL.Context
{
    public class AppDbContext : DbContext
    {
        #region DbSets
        // DbSets for the entities in the application
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<OrderItemTopping> OrderItemToppings { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Combo> Combos { get; set; }
        public DbSet<ComboItem> ComboItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductVariant> ProductVariants { get; set; }
        public DbSet<ProductVariantTopping> ProductVariantToppings { get; set; }
        public DbSet<Topping> Toppings { get; set; }
        public DbSet<Size> Sizes { get; set; }
        #endregion


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }

        public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
        {
            public AppDbContext CreateDbContext(string[] args)
            {

                string presentationDir = @"D:\fpt\B3W\MilkteaShop\MilkteaShop-BE\Presentation";

                // Ensure the directory exists
                if (!Directory.Exists(presentationDir))
                {
                    throw new DirectoryNotFoundException($"Presentation directory not found at {presentationDir}");
                }

                // Build config from the Presentation project location
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(presentationDir)
                    .AddJsonFile("appsettings.json", optional: false)
                    .Build();

                var connectionString = configuration.GetConnectionString("DefaultConnection");

                var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
                optionsBuilder.UseSqlServer(connectionString);

                return new AppDbContext(optionsBuilder.Options);
            }
        }   



    }
    
    
}
