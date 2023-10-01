using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebAPItest.Models;

public partial class A01Context : DbContext
{
    public A01Context(DbContextOptions<A01Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Favorite> Favorites { get; set; }

    public virtual DbSet<Furniture> Furnitures { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.BrandId).HasName("PK__Brand__06B772993166DED0");

            entity.ToTable("Brand");

            entity.Property(e => e.BrandId).HasColumnName("brandId");
            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.Brand1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("brand");
            entity.Property(e => e.Logo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("logo");
            entity.Property(e => e.PhoneNumber).HasColumnName("phoneNumber");
        });

        modelBuilder.Entity<Furniture>(entity =>
        {
            entity.HasKey(e => e.FurnitureId).HasName("PK__Furnitur__BD41E4C59D892524");

            entity.ToTable("Furniture");

            entity.Property(e => e.FurnitureId).HasColumnName("furnitureId");
            entity.Property(e => e.BrandId).HasColumnName("brandId");
            entity.Property(e => e.Color)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("color");
            entity.Property(e => e.Location)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("location");
            entity.Property(e => e.Picture)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("picture");
            entity.Property(e => e.Style)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("style");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("type");

            entity.HasOne(d => d.Brand).WithMany()
                .HasForeignKey(d => d.BrandId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Furniture__brand__286302EC");
        });

        modelBuilder.Entity<Favorite>(entity =>
        {
            entity.HasKey(e => e.FavoriteId).HasName("PK__Favorite");

            entity.ToTable("Favorite");

            entity.Property(e => e.FurnitureId).HasColumnName("furnitureId");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.Furniture).WithMany()
                .HasForeignKey(d => d.FurnitureId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Favorite__furnit__2B3F6F97");

        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
