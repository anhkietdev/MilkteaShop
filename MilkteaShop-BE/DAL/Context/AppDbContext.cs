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
        #endregion

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Store Relationships
            modelBuilder.Entity<Store>(entity =>
            {
                // Store - Users (One-to-Many)
                entity.HasMany(s => s.Users)
                      .WithOne(u => u.Store)
                      .HasForeignKey(u => u.StoreId)
                      .OnDelete(DeleteBehavior.SetNull);

                // Store - Orders (One-to-Many)
                entity.HasMany(s => s.Orders)
                      .WithOne(o => o.Store)
                      .HasForeignKey(o => o.StoreId)
                      .OnDelete(DeleteBehavior.SetNull);

                // Soft Delete Filter
                entity.HasQueryFilter(e => e.DeletedAt == null);
            });

            // Category Relationships
            modelBuilder.Entity<Category>(entity =>
            {
                // Category - Product (One-to-Many)
                entity.HasMany(c => c.Products)
                      .WithOne(p => p.Category)
                      .HasForeignKey(p => p.CategoryId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Category - CategoryExtraMapping (One-to-Many)
                entity.HasMany(c => c.CategoryExtraMappings)
                      .WithOne(m => m.MainCategory)
                      .HasForeignKey(m => m.MainCategoryId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Soft Delete Filter
                entity.HasQueryFilter(e => e.DeletedAt == null);
            });

            // CategoryExtraMapping Relationships
            modelBuilder.Entity<CategoryExtraMapping>(entity =>
            {
                // CategoryExtraMapping - MainCategory (Many-to-One)
                entity.HasOne(m => m.MainCategory)
                      .WithMany()
                      .HasForeignKey(m => m.MainCategoryId)
                      .OnDelete(DeleteBehavior.Restrict);

                // CategoryExtraMapping - ExtraCategory (Many-to-One)
                entity.HasOne(m => m.ExtraCategory)
                      .WithMany()
                      .HasForeignKey(m => m.ExtraCategoryId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Soft Delete Filter
                entity.HasQueryFilter(e => e.DeletedAt == null);
            });

            // Product Relationships
            modelBuilder.Entity<Product>(entity =>
            {
                // Soft Delete Filter
                entity.HasQueryFilter(e => e.DeletedAt == null);
            });

            // ComboItem Relationships
            modelBuilder.Entity<ComboItem>(entity =>
            {
                // ComboItem - Product (Many-to-One) - already defined in Product

                // Soft Delete Filter
                entity.HasQueryFilter(e => e.DeletedAt == null);
            });

            // Order Relationships
            modelBuilder.Entity<Order>(entity =>
            {
                // Order - OrderItem (One-to-Many)
                entity.HasMany(o => o.OrderItems)
                      .WithOne(oi => oi.Order)
                      .HasForeignKey(oi => oi.OrderId)
                      .OnDelete(DeleteBehavior.Cascade);

                // Order - Store (Many-to-One)
                entity.HasOne(o => o.Store)
                      .WithMany(s => s.Orders)
                      .HasForeignKey(o => o.StoreId)
                      .OnDelete(DeleteBehavior.SetNull);

                // Soft Delete Filter
                entity.HasQueryFilter(e => e.DeletedAt == null);
            });

            // OrderItem Relationships
            modelBuilder.Entity<OrderItem>(entity =>
            {
                // OrderItem - Order (Many-to-One) - already defined in Order

                // OrderItem - Product (Many-to-One) - already defined in Product

                // OrderItem - OrderItem (Self-referencing for toppings)
                entity.HasOne(o => o.ParentOrderItem)
                      .WithMany(o => o.ToppingItems)
                      .HasForeignKey(o => o.ParentOrderItemId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Soft Delete Filter
                entity.HasQueryFilter(e => e.DeletedAt == null);
            });

            // User Relationships
            modelBuilder.Entity<User>(entity =>
            {
                // User - Store (Many-to-One)
                entity.HasOne(u => u.Store)
                      .WithMany(s => s.Users)
                      .HasForeignKey(u => u.StoreId)
                      .OnDelete(DeleteBehavior.SetNull);

                // Soft Delete Filter
                entity.HasQueryFilter(e => e.DeletedAt == null);
            });
        }
    }
}