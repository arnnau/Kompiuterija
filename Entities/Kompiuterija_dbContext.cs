using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Kompiuterija.Entities
***REMOVED***
    public partial class Kompiuterija_dbContext : DbContext
    ***REMOVED***
        public Kompiuterija_dbContext()
        ***REMOVED***
***REMOVED***

        public Kompiuterija_dbContext(DbContextOptions<Kompiuterija_dbContext> options)
            : base(options)
        ***REMOVED***
***REMOVED***

        public virtual DbSet<Computer> Computer ***REMOVED*** get; set; ***REMOVED***
        public virtual DbSet<Part> Part ***REMOVED*** get; set; ***REMOVED***
        public virtual DbSet<Shop> Shop ***REMOVED*** get; set; ***REMOVED***
        public virtual DbSet<User> User ***REMOVED*** get; set; ***REMOVED***

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        ***REMOVED***
            if (!optionsBuilder.IsConfigured)
            ***REMOVED***
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
    ***REMOVED***
***REMOVED***

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        ***REMOVED***
            modelBuilder.Entity<Computer>(entity =>
            ***REMOVED***
                entity.ToTable("computer");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255);

                entity.Property(e => e.Registered)
                    .HasColumnName("registered")
                    .HasColumnType("date");

                entity.Property(e => e.ShopId).HasColumnName("shop_id");

                entity.Property(e => e.User)
                    .IsRequired()
                    .HasColumnName("user")
                    .HasMaxLength(255);
    ***REMOVED***);

            modelBuilder.Entity<Part>(entity =>
            ***REMOVED***
                entity.ToTable("part");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ComputerId).HasColumnName("computer_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnName("type")
                    .HasMaxLength(255);
    ***REMOVED***);

            modelBuilder.Entity<Shop>(entity =>
            ***REMOVED***
                entity.ToTable("shop");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasMaxLength(255);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasColumnName("city")
                    .HasMaxLength(255);
    ***REMOVED***);

            modelBuilder.Entity<User>(entity =>
            ***REMOVED***
                entity.HasKey(e => e.Email)
                    .HasName("PRIMARY");

                entity.ToTable("user");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(255);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(255);

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasColumnName("role")
                    .HasMaxLength(255);
    ***REMOVED***);

            OnModelCreatingPartial(modelBuilder);
***REMOVED***

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
***REMOVED***
***REMOVED***
