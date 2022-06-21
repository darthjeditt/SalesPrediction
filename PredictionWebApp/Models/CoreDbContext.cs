using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PredictionWebApp.Models
{
    public partial class CoreDbContext : DbContext
    {
        public CoreDbContext()
        {
        }

        public CoreDbContext(DbContextOptions<CoreDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblSales> TblSales { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-9MT5TES;Database=SalesHistoryDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblSales>(entity =>
            {
                entity.HasKey(e => e.SalesId)
                    .HasName("PK__Sales__C952FB123BED40E5");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
