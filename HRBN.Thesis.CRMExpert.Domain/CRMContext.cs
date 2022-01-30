using System;
using HRBN.Thesis.CRMExpert.Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace HRBN.Thesis.CRMExpert.Domain
{
    public partial class CRMContext : DbContext
    {
        public CRMContext()
        {
        }

        public CRMContext(DbContextOptions<CRMContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Discount> Discounts { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Todo> Todos { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost;Database=CRM;User Id=SA;Password=hAsElKo123@;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ContactComment).HasMaxLength(2048);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.CreDate).HasColumnType("datetime");
                
                entity.Property(e => e.ModDate).HasColumnType("datetime");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.PostalCode)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Street)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Contacts)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Contacts_Users");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Contacts)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Contacts_Customers");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
                
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128);
                
                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(128);
                
                entity.Property(e => e.Street)
                    .IsRequired()
                    .HasMaxLength(128);
                
                entity.Property(e => e.PostalCode)
                    .IsRequired()
                    .HasMaxLength(128);
                
                entity.Property(e => e.TaxNo)
                    .IsRequired()
                    .HasMaxLength(128);
                
                entity.Property(e => e.Regon).HasMaxLength(128);
                
                entity.Property(e => e.CreDate).HasColumnType("datetime");

                entity.Property(e => e.ModDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Discount>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
                
                entity.Property(e => e.DiscountVaule)
                    .HasColumnType("money")
                    .IsRequired();
                
                entity.Property(e => e.CreDate).HasColumnType("datetime");
                
                entity.Property(e => e.ModDate).HasColumnType("datetime");
                
                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Discounts)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Discounts_Products");
                
                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Discounts)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Discounts_Customers");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Content).HasMaxLength(2048);

                entity.Property(e => e.CreDate).HasColumnType("datetime");

                entity.Property(e => e.ModDate).HasColumnType("datetime");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ContactId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Orders_Contacts");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Orders_Products");
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
                
                entity.Property(e => e.CreDate).HasColumnType("datetime");
                
                entity.Property(e => e.ModDate).HasColumnType("datetime");
                
                entity.HasOne(d => d.User)
                    .WithMany(p => p.Permissions)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Permissions_Users");
                
                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Permissions)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Permissions_Roles");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
                
                entity.Property(e => e.Name).HasMaxLength(128);
                
                entity.Property(e => e.Price).HasColumnType("money");
                
                entity.Property(e => e.Description).HasMaxLength(2048);
                
                entity.Property(e => e.Type).HasMaxLength(128);
                
                entity.Property(e => e.Count).HasColumnType("money");
                
                entity.Property(e => e.CreDate).HasColumnType("datetime");

                entity.Property(e => e.ModDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
                
                entity.Property(e => e.CreDate).HasColumnType("datetime");

                entity.Property(e => e.ModDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Todo>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Content).HasMaxLength(2048);

                entity.Property(e => e.CreDate).HasColumnType("datetime");

                entity.Property(e => e.ModDate).HasColumnType("datetime");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.Todos)
                    .HasForeignKey(d => d.ContactId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Todos_Contacts");
                
                entity.HasOne(d => d.User)
                    .WithMany(p => p.Todos)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Todos_Users");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreDate).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(1);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.ModDate).HasColumnType("datetime");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.PostalCode)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Street)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(64);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
