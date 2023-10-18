using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebAPItest.Models;

public partial class A01Context : DbContext
{
    public A01Context()
    {
    }

    public A01Context(DbContextOptions<A01Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Branch> Branches { get; set; }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Favorite> Favorites { get; set; }

    public virtual DbSet<Furniture> Furnitures { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Branch>(entity =>
        {
            entity.HasKey(e => e.BranchId).HasName("PK__Branch__751EBD5F33FA43BF");

            entity.ToTable("Branch");

            entity.Property(e => e.BranchId).HasColumnName("branchId");
            entity.Property(e => e.Address)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.BranchName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("branchName");
            entity.Property(e => e.BrandId).HasColumnName("brandId");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("phoneNumber");

            entity.HasOne(d => d.Brand).WithMany(p => p.Branches)
                .HasForeignKey(d => d.BrandId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Branch__brandId__4E88ABD4");
        });

        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.BrandId).HasName("PK__Brand__06B772995AD10E28");

            entity.ToTable("Brand");

            entity.Property(e => e.BrandId).HasColumnName("brandId");
            entity.Property(e => e.Address)
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
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("phoneNumber");
            entity.Property(e => e.Url)
                .IsUnicode(false)
                .HasColumnName("url");
        });

        modelBuilder.Entity<Favorite>(entity =>
        {
            entity.HasKey(e => e.FavoriteId).HasName("PK__Favorite__876A64D58AA15765");

            entity.ToTable("Favorite");

            entity.Property(e => e.FavoriteId).HasColumnName("favoriteId");
            entity.Property(e => e.FurnitureId).HasColumnName("furnitureId");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.Furniture).WithMany(p => p.Favorites)
                .HasForeignKey(d => d.FurnitureId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Favorite__furnit__59FA5E80");
        });

        modelBuilder.Entity<Furniture>(entity =>
        {
            entity.HasKey(e => e.FurnitureId).HasName("PK__Furnitur__BD41E4C50FA6B547");

            entity.ToTable("Furniture");

            entity.Property(e => e.FurnitureId).HasColumnName("furnitureId");
            entity.Property(e => e.BrandId).HasColumnName("brandId");
            entity.Property(e => e.Color)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("color");
            entity.Property(e => e.Location)
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

            entity.HasOne(d => d.Brand).WithMany(p => p.Furnitures)
                .HasForeignKey(d => d.BrandId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Furniture__brand__571DF1D5");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.MessageId).HasName("PK__Message__4808B9933BCF5750");

            entity.ToTable("Message");

            entity.Property(e => e.MessageId)
                .ValueGeneratedNever()
                .HasColumnName("messageId");
            entity.Property(e => e.FurnitureId).HasColumnName("furnitureId");
            entity.Property(e => e.IsDelete).HasColumnName("isDelete");
            entity.Property(e => e.Message1).HasColumnName("message");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.Furniture).WithMany(p => p.Messages)
                .HasForeignKey(d => d.FurnitureId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Message__furnitu__5CD6CB2B");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
