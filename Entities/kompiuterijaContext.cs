﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Kompiuterija.Entities
***REMOVED***
    public partial class kompiuterijaContext : DbContext
    ***REMOVED***
        public kompiuterijaContext()
        ***REMOVED***
***REMOVED***

        public kompiuterijaContext(DbContextOptions<kompiuterijaContext> options)
            : base(options)
        ***REMOVED***
***REMOVED***

        public virtual DbSet<Computer> Computer ***REMOVED*** get; set; ***REMOVED***
        public virtual DbSet<Part> Part ***REMOVED*** get; set; ***REMOVED***
        public virtual DbSet<Shop> Shop ***REMOVED*** get; set; ***REMOVED***

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        ***REMOVED***
            if (!optionsBuilder.IsConfigured)
            ***REMOVED***
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySQL("server=localhost;port=3306;user=user;password=pass;database=kompiuterija");
    ***REMOVED***
***REMOVED***

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        ***REMOVED***
            modelBuilder.Entity<Computer>(entity =>
            ***REMOVED***
                entity.ToTable("computer");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255);

                entity.Property(e => e.Registered)
                    .HasColumnName("registered")
                    .HasColumnType("date");

                entity.Property(e => e.ShopId)
                    .HasColumnName("shop_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("int(11)");
    ***REMOVED***);

            modelBuilder.Entity<Part>(entity =>
            ***REMOVED***
                entity.ToTable("part");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ComputerId)
                    .HasColumnName("computer_id")
                    .HasColumnType("int(11)");

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

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasMaxLength(255);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasColumnName("city")
                    .HasMaxLength(255);
    ***REMOVED***);

            OnModelCreatingPartial(modelBuilder);
***REMOVED***

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
***REMOVED***
***REMOVED***
