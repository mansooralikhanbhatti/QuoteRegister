using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace QuoteRegister.Entities;

public partial class QuoteTrackingDBContext : DbContext
{
    public QuoteTrackingDBContext()
    {
    }

    public QuoteTrackingDBContext(DbContextOptions<QuoteTrackingDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Quote> Quotes { get; set; }

    public virtual DbSet<QuoteItem> QuoteItems { get; set; }

    public virtual DbSet<SalesStaff> SalesStaffs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64D8EF97A1A0");

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Quote>(entity =>
        {
            entity.HasKey(e => e.QuoteId).HasName("PK__Quotes__AF9688C11D60E456");

            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.RecievedDate).HasColumnType("datetime");
            entity.Property(e => e.Title).HasMaxLength(200);

            entity.HasOne(d => d.RecievedByStaff).WithMany(p => p.Quotes)
                .HasForeignKey(d => d.RecievedByStaffId)
                .HasConstraintName("FK__Quotes__Recieved__3B75D760");

            entity.HasOne(d => d.RequestedByCustomer).WithMany(p => p.Quotes)
                .HasForeignKey(d => d.RequestedByCustomerId)
                .HasConstraintName("FK__Quotes__Requeste__3C69FB99");
        });

        modelBuilder.Entity<QuoteItem>(entity =>
        {
            entity.HasKey(e => e.QuoteItemId).HasName("PK__QuoteIte__B0ED34FF6D07C4D4");

            entity.Property(e => e.ItemCode).HasMaxLength(50);
            entity.Property(e => e.ItemName).HasMaxLength(100);

            entity.HasOne(d => d.Quote).WithMany(p => p.QuoteItems)
                .HasForeignKey(d => d.QuoteId)
                .HasConstraintName("FK__QuoteItem__Quote__3F466844");
        });

        modelBuilder.Entity<SalesStaff>(entity =>
        {
            entity.HasKey(e => e.StaffId).HasName("PK__SalesSta__96D4AB17E037D3F4");

            entity.ToTable("SalesStaff");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
