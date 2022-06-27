using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GroceryApi
{
    public partial class dbGroceryContext : DbContext
    {
        public dbGroceryContext()
        {
        }

        public dbGroceryContext(DbContextOptions<dbGroceryContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Login> Logins { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Sale> Sales { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=dbGrocery;Integrated Security=True;");
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Login>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Login");

                entity.Property(e => e.Password)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.ProductId).HasColumnName("Product_Id");

                entity.Property(e => e.Company)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .HasMaxLength(40)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Sale>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PK__Sales__F1E4607B305E2931");

                entity.Property(e => e.OrderId).HasColumnName("Order_Id");

                entity.Property(e => e.CustomerAddress)
                    .HasMaxLength(35)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerName)
                    .HasMaxLength(35)
                    .IsUnicode(false);

                entity.Property(e => e.Date)
                    .HasMaxLength(35)
                    .IsUnicode(false);

                entity.Property(e => e.Mobile)
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
