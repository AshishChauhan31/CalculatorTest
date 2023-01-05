using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CalculatorTest.Models;

public partial class CalculatorDatabaseContext : DbContext
{
    public CalculatorDatabaseContext()
    {
    }

    public CalculatorDatabaseContext(DbContextOptions<CalculatorDatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblCalculation> TblCalculations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=CalculatorDatabase;Trusted_Connection=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblCalculation>(entity =>
        {
            entity.ToTable("Tbl_Calculation");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.FinalResult)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Input)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
