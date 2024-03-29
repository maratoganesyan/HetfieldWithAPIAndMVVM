﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace APIForHetfield.Models;

public partial class Db : DbContext
{
    public Db()
    {
    }

    public Db(DbContextOptions<Db> options)
        : base(options)
    {
    }

    public virtual DbSet<Car> Cars { get; set; }

    public virtual DbSet<CarColor> CarColors { get; set; }

    public virtual DbSet<CarConfiguration> CarConfigurations { get; set; }

    public virtual DbSet<CarEngine> CarEngines { get; set; }

    public virtual DbSet<CarPhoto> CarPhotos { get; set; }

    public virtual DbSet<CarStatus> CarStatuses { get; set; }

    public virtual DbSet<CarTranssmission> CarTranssmissions { get; set; }

    public virtual DbSet<CarType> CarTypes { get; set; }

    public virtual DbSet<CarsPassport> CarsPassports { get; set; }

    public virtual DbSet<Gender> Genders { get; set; }

    public virtual DbSet<ManufactureYear> ManufactureYears { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderStatus> OrderStatuses { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
        optionsBuilder.UseSqlServer("Data Source=DESKTOP-S7OLEVV\\SQLEXPRESS;Initial Catalog=HetfieldDuplicate;Integrated Security=True; Encrypt=false; MultipleActiveResultSets=true");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Car>(entity =>
        {
            entity.HasKey(e => e.IdCar);

            entity.Property(e => e.CarNumber)
                .IsRequired()
                .HasMaxLength(10);
            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(3000);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdCarConfigurationNavigation).WithMany(p => p.Cars)
                .HasForeignKey(d => d.IdCarConfiguration)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cars_CarConfigurations");

            entity.HasOne(d => d.IdCarPassportNavigation).WithMany(p => p.Cars)
                .HasForeignKey(d => d.IdCarPassport)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cars_CarsPassport");

            entity.HasOne(d => d.IdCarStatusNavigation).WithMany(p => p.Cars)
                .HasForeignKey(d => d.IdCarStatus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cars_CarStatuses");

            entity.HasOne(d => d.IdEngineNavigation).WithMany(p => p.Cars)
                .HasForeignKey(d => d.IdEngine)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cars_CarEngines");

            entity.HasOne(d => d.IdTranssmissionNavigation).WithMany(p => p.Cars)
                .HasForeignKey(d => d.IdTranssmission)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cars_CarTranssmissions");
        });

        modelBuilder.Entity<CarColor>(entity =>
        {
            entity.HasKey(e => e.IdCarColors);

            entity.Property(e => e.ColorName)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.Hex)
                .IsRequired()
                .HasMaxLength(10)
                .HasColumnName("HEX");
        });

        modelBuilder.Entity<CarConfiguration>(entity =>
        {
            entity.HasKey(e => e.IdCarConfiguration);

            entity.Property(e => e.CarConfigurationName)
                .IsRequired()
                .HasMaxLength(20);
        });

        modelBuilder.Entity<CarEngine>(entity =>
        {
            entity.HasKey(e => e.IdCarEngine);

            entity.Property(e => e.EngineName)
                .IsRequired()
                .HasMaxLength(20);
        });

        modelBuilder.Entity<CarPhoto>(entity =>
        {
            entity.HasKey(e => new { e.IdCar, e.IdPhoto });

            entity.Property(e => e.Photo).IsRequired();

            entity.HasOne(d => d.IdCarNavigation).WithMany(p => p.CarPhotos)
                .HasForeignKey(d => d.IdCar)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CarPhotos_Cars");
        });

        modelBuilder.Entity<CarStatus>(entity =>
        {
            entity.HasKey(e => e.IdCarStatus);

            entity.Property(e => e.CarStatusName)
                .IsRequired()
                .HasMaxLength(20);
        });

        modelBuilder.Entity<CarTranssmission>(entity =>
        {
            entity.HasKey(e => e.IdTranssmission);

            entity.Property(e => e.TranssmissionName)
                .IsRequired()
                .HasMaxLength(25);
        });

        modelBuilder.Entity<CarType>(entity =>
        {
            entity.HasKey(e => e.IdCarType);

            entity.Property(e => e.TypeName)
                .IsRequired()
                .HasMaxLength(20);
        });

        modelBuilder.Entity<CarsPassport>(entity =>
        {
            entity.HasKey(e => e.IdCarPassport);

            entity.ToTable("CarsPassport");

            entity.Property(e => e.CarModel)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.DateOfIssue).HasColumnType("date");
            entity.Property(e => e.PassportSeriasAndNumber)
                .IsRequired()
                .HasMaxLength(12);
            entity.Property(e => e.VinNumber)
                .IsRequired()
                .HasMaxLength(17);

            entity.HasOne(d => d.CarManufactureYearNavigation).WithMany(p => p.CarsPassports)
                .HasForeignKey(d => d.CarManufactureYear)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CarsPassport_ManufactureYears");

            entity.HasOne(d => d.IdCarColorNavigation).WithMany(p => p.CarsPassports)
                .HasForeignKey(d => d.IdCarColor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CarsPassport_CarColors");

            entity.HasOne(d => d.IdCarTypeNavigation).WithMany(p => p.CarsPassports)
                .HasForeignKey(d => d.IdCarType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CarsPassport_CarTypes");

            entity.HasOne(d => d.IdOwnerNavigation).WithMany(p => p.CarsPassports)
                .HasForeignKey(d => d.IdOwner)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CarsPassport_Users");
        });

        modelBuilder.Entity<Gender>(entity =>
        {
            entity.HasKey(e => e.IdGender);

            entity.Property(e => e.GenderName)
                .IsRequired()
                .HasMaxLength(7);
        });

        modelBuilder.Entity<ManufactureYear>(entity =>
        {
            entity.HasKey(e => e.YearValue).HasName("PK_ManifactureYears");

            entity.Property(e => e.YearValue).ValueGeneratedNever();
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.IdOrder);

            entity.Property(e => e.DateOfOrder).HasColumnType("date");
            entity.Property(e => e.FinalPrice).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdBuyerNavigation).WithMany(p => p.OrderIdBuyerNavigations)
                .HasForeignKey(d => d.IdBuyer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Orders_Users");

            entity.HasOne(d => d.IdCarNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.IdCar)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Orders_Cars");

            entity.HasOne(d => d.IdOrderStatusNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.IdOrderStatus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Orders_OrderStatuses");

            entity.HasOne(d => d.IdStaffNavigation).WithMany(p => p.OrderIdStaffNavigations)
                .HasForeignKey(d => d.IdStaff)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Orders_Users1");
        });

        modelBuilder.Entity<OrderStatus>(entity =>
        {
            entity.HasKey(e => e.IdOrderStatus);

            entity.Property(e => e.OrderStatusName)
                .IsRequired()
                .HasMaxLength(20);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRole);

            entity.Property(e => e.RoleName)
                .IsRequired()
                .HasMaxLength(25);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser);

            entity.Property(e => e.DateOfBirth).HasColumnType("date");
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.Login)
                .IsRequired()
                .HasMaxLength(20);
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(20);
            entity.Property(e => e.Patronymic).HasMaxLength(50);
            entity.Property(e => e.PhoneNumber)
                .IsRequired()
                .HasMaxLength(18);
            entity.Property(e => e.Photo).IsRequired();
            entity.Property(e => e.Surname)
                .IsRequired()
                .HasMaxLength(50);

            entity.HasOne(d => d.IdGenderNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdGender)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_Genders");

            entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdRole)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_Roles");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}