using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DLNLTT
{
    public partial class DLNLTTContext : DbContext
    {
        public DLNLTTContext()
        {
        }

        public DLNLTTContext(DbContextOptions<DLNLTTContext> options)
            : base(options)
        {
        }

        public virtual DbSet<SoLieu> SoLieus { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=LAPTOP-TMG7DMND\\SQLEXPRESS;Database=DLNLTT;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<SoLieu>(entity =>
            {
                entity.ToTable("SoLieu");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.GCsln)
                    .HasMaxLength(50)
                    .HasColumnName("G_CSLN");

                entity.Property(e => e.GHt)
                    .HasMaxLength(50)
                    .HasColumnName("G_HT");

                entity.Property(e => e.GSln)
                    .HasMaxLength(50)
                    .HasColumnName("G_SLN");

                entity.Property(e => e.GTk)
                    .HasMaxLength(50)
                    .HasColumnName("G_TK");

                entity.Property(e => e.MtCsln)
                    .HasMaxLength(50)
                    .HasColumnName("MT_CSLN");

                entity.Property(e => e.MtHt)
                    .HasMaxLength(50)
                    .HasColumnName("MT_HT");

                entity.Property(e => e.MtSln)
                    .HasMaxLength(50)
                    .HasColumnName("MT_SLN");

                entity.Property(e => e.MtTk)
                    .HasMaxLength(50)
                    .HasColumnName("MT_TK");

                entity.Property(e => e.SkCsln)
                    .HasMaxLength(50)
                    .HasColumnName("SK_CSLN");

                entity.Property(e => e.SkHt)
                    .HasMaxLength(50)
                    .HasColumnName("SK_HT");

                entity.Property(e => e.SkSln)
                    .HasMaxLength(50)
                    .HasColumnName("SK_SLN");

                entity.Property(e => e.SkTk)
                    .HasMaxLength(50)
                    .HasColumnName("SK_TK");

                entity.Property(e => e.TCsln)
                    .HasMaxLength(50)
                    .HasColumnName("T_CSLN");

                entity.Property(e => e.THt)
                    .HasMaxLength(50)
                    .HasColumnName("T_HT");

                entity.Property(e => e.TSln)
                    .HasMaxLength(50)
                    .HasColumnName("T_SLN");

                entity.Property(e => e.TTk)
                    .HasMaxLength(50)
                    .HasColumnName("T_TK");

                entity.Property(e => e.ThoiGian)
                    .HasMaxLength(50)
                    .HasColumnName("ThoiGian");


            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
