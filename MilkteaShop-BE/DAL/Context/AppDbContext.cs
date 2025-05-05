using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Context
{
    public class AppDbContext : DbContext
    {
        #region DbSets
        // DbSets for the entities in the application
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryExtraMapping> CategoryExtraMappings { get; set; }
        public DbSet<ComboItem> ComboItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<ComboItemProductSize> ComboItemProductSizes { get; set; }

        public DbSet<ProductSize> ProductSize { get; set; }
        public DbSet<OrderItemTopping> OrderItemToppings { get; set; }

        #endregion

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Store>(entity =>
            {
                entity.HasMany(s => s.Users)
                      .WithOne(u => u.Store)
                      .HasForeignKey(u => u.StoreId)
                      .OnDelete(DeleteBehavior.SetNull);

                entity.HasMany(s => s.Orders)
                      .WithOne(o => o.Store)
                      .HasForeignKey(o => o.StoreId)
                      .OnDelete(DeleteBehavior.SetNull);

                entity.HasQueryFilter(e => e.DeletedAt == null);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasMany(c => c.Products)
                      .WithOne(p => p.Category)
                      .HasForeignKey(p => p.CategoryId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(c => c.CategoryExtraMappings)
                      .WithOne(m => m.MainCategory)
                      .HasForeignKey(m => m.MainCategoryId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasQueryFilter(e => e.DeletedAt == null);
            });

            modelBuilder.Entity<CategoryExtraMapping>(entity =>
            {
                entity.HasOne(m => m.MainCategory)
                      .WithMany()
                      .HasForeignKey(m => m.MainCategoryId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(m => m.ExtraCategory)
                      .WithMany()
                      .HasForeignKey(m => m.ExtraCategoryId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasQueryFilter(e => e.DeletedAt == null);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasQueryFilter(e => e.DeletedAt == null);
            });

            modelBuilder.Entity<ComboItem>(entity =>
            {
                entity.HasQueryFilter(e => e.DeletedAt == null);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasMany(o => o.OrderItems)
                      .WithOne(oi => oi.Order)
                      .HasForeignKey(oi => oi.OrderId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(o => o.Store)
                      .WithMany(s => s.Orders)
                      .HasForeignKey(o => o.StoreId)
                      .OnDelete(DeleteBehavior.SetNull);

                entity.HasQueryFilter(e => e.DeletedAt == null);
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasQueryFilter(e => e.DeletedAt == null);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasOne(u => u.Store)
                      .WithMany(s => s.Users)
                      .HasForeignKey(u => u.StoreId)
                      .OnDelete(DeleteBehavior.SetNull);

                entity.HasQueryFilter(e => e.DeletedAt == null);
            });

            modelBuilder.Entity<OrderItemTopping>()
                .HasKey(t => new { t.OrderItemId, t.ProductSizeId });

            modelBuilder.Entity<OrderItemTopping>()
                .HasOne(t => t.OrderItem)
                .WithMany(oi => oi.Toppings)
                .HasForeignKey(t => t.OrderItemId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderItemTopping>()
                .HasOne(t => t.ProductSize)
                .WithMany(ps => ps.UsedAsToppingIn)
                .HasForeignKey(t => t.ProductSizeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}