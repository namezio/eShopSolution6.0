using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace eShop.Database
{
    public partial class eShopEntities : DbContext
    {
        public eShopEntities()
        {
        }

        public eShopEntities(DbContextOptions<eShopEntities> options)
            : base(options)
        {
        }

        public virtual DbSet<Option> Options { get; set; }
        public virtual DbSet<OptionGroup> OptionGroups { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }
        public virtual DbSet<ProductOption> ProductOptions { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySQL("Host=127.0.0.1;Database=eShopDb;Username=root;Password=Kobiet99");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Option>(entity =>
            {
                entity.ToTable("Options", "eShopDb");

                entity.Property(e => e.OptionName).HasMaxLength(50);
            });

            modelBuilder.Entity<OptionGroup>(entity =>
            {
                entity.ToTable("OptionGroups", "eShopDb");

                entity.Property(e => e.OptionGroupName).HasMaxLength(50);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Orders", "eShopDb");

                entity.HasIndex(e => e.OrderUserId, "OrderUserId");

                entity.Property(e => e.OrderAddress)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.OrderName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.OrderPhone)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.HasOne(d => d.OrderUser)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.OrderUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("orders_ibfk_1");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(e => e.DetailId)
                    .HasName("PRIMARY");

                entity.ToTable("OrderDetails", "eShopDb");

                entity.HasIndex(e => e.DetailName, "DetailName");

                entity.HasIndex(e => e.DetailOrderId, "DetailOrderId");

                entity.HasIndex(e => e.DetailProductId, "DetailProductId");

                entity.Property(e => e.DetailName)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.HasOne(d => d.DetailOrder)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.DetailOrderId)
                    .HasConstraintName("orderdetails_ibfk_1");

                entity.HasOne(d => d.DetailProduct)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.DetailProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("orderdetails_ibfk_2");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Products", "eShopDb");

                entity.HasIndex(e => e.ProductName, "ProductName");

                entity.HasIndex(e => e.ProductCategoryId, "products_ibfk_1");

                entity.Property(e => e.ProductCartDesc).HasMaxLength(250);

                entity.Property(e => e.ProductImage).HasMaxLength(1000);

                entity.Property(e => e.ProductLocation).HasMaxLength(250);

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ProductShortDesc).HasMaxLength(1000);

                entity.Property(e => e.ProductThumb).HasMaxLength(100);

                entity.HasOne(d => d.ProductCategory)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ProductCategoryId)
                    .HasConstraintName("products_ibfk_1");
            });

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.HasKey(e => e.CategoryId)
                    .HasName("PRIMARY");

                entity.ToTable("ProductCategories", "eShopDb");

                entity.Property(e => e.CategoryName).HasMaxLength(50);
            });

            modelBuilder.Entity<ProductOption>(entity =>
            {
                entity.ToTable("ProductOptions", "eShopDb");

                entity.HasIndex(e => e.OptionGroupId, "OptionGroupId");

                entity.HasIndex(e => e.OptionId, "OptionId");

                entity.HasIndex(e => e.ProductId, "ProductId");

                entity.HasOne(d => d.OptionGroup)
                    .WithMany(p => p.ProductOptions)
                    .HasForeignKey(d => d.OptionGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("productoptions_ibfk_3");

                entity.HasOne(d => d.Option)
                    .WithMany(p => p.ProductOptions)
                    .HasForeignKey(d => d.OptionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("productoptions_ibfk_1");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductOptions)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("productoptions_ibfk_4");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users", "eShopDb");

                entity.Property(e => e.UserAddress)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.UserEmail)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.UserFirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UserLastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UserPassword)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.UserPhone)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
