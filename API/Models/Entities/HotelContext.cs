﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace API.Models.Entities
{
    public partial class HotelContext : DbContext
    {
        public HotelContext()
        {
        }

        public HotelContext(DbContextOptions<HotelContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<Booking> Booking { get; set; }
        public virtual DbSet<OrderDetails> OrderDetails { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Room> Room { get; set; }
        public virtual DbSet<Services> Services { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.Property(e => e.Address).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.Property(e => e.DateEnd).HasColumnType("date");

                entity.Property(e => e.DateStart).HasColumnType("date");

                entity.Property(e => e.RoomId).HasColumnName("RoomID");

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.HasOne(d => d.IdCustomerNavigation)
                    .WithMany(p => p.Booking)
                    .HasForeignKey(d => d.IdCustomer)
                    .HasConstraintName("FK_Booking_Account");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Booking)
                    .HasForeignKey(d => d.RoomId)
                    .HasConstraintName("FK_Booking_Room");
            });

            modelBuilder.Entity<OrderDetails>(entity =>
            {
                entity.HasKey(e => new { e.ServicesId, e.OrderId });

                entity.Property(e => e.ServicesId).HasColumnName("ServicesID");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDetails_Orders");

                entity.HasOne(d => d.Services)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ServicesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDetails_Services");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.DateEnd).HasColumnType("datetime");

                entity.Property(e => e.DateStart).HasColumnType("datetime");

                entity.Property(e => e.Total).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.IdCusNavigation)
                    .WithMany(p => p.OrdersIdCusNavigation)
                    .HasForeignKey(d => d.IdCus)
                    .HasConstraintName("FK_Orders_Account");

                entity.HasOne(d => d.IdEmpNavigation)
                    .WithMany(p => p.OrdersIdEmpNavigation)
                    .HasForeignKey(d => d.IdEmp)
                    .HasConstraintName("FK_Orders_Account1");

                entity.HasOne(d => d.IdRoomNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.IdRoom)
                    .HasConstraintName("FK_Orders_Room");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.IdEmpNavigation)
                    .WithMany(p => p.Room)
                    .HasForeignKey(d => d.IdEmp)
                    .HasConstraintName("FK_Room_Account");
            });

            modelBuilder.Entity<Services>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
