using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Asset_management_system_DAL.Models
{
    public partial class AssetManagementSystemDBContext : DbContext
    {
        public AssetManagementSystemDBContext()
        {
        }

        public AssetManagementSystemDBContext(DbContextOptions<AssetManagementSystemDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AssetFolderStructure> AssetFolderStructures { get; set; }
        public virtual DbSet<AssetMetadatum> AssetMetadata { get; set; }
        public virtual DbSet<AssetVariant> AssetVariants { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Server=localhost;Database=AssetManagementSystemDB;Trusted_Connection=True;");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AI");

            modelBuilder.Entity<AssetFolderStructure>(entity =>
            {
                entity.HasKey(e => e.AssetId);

                entity.ToTable("AssetFolderStructure");

                entity.Property(e => e.AssetId).HasColumnName("AssetID");

                entity.Property(e => e.AssetName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BlobStoragePath)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ParentId).HasColumnName("ParentID");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_AssetFolderStructure_Parent");
            });

            modelBuilder.Entity<AssetMetadatum>(entity =>
            {
                entity.HasKey(e => e.AssetMetaDataId);

                entity.Property(e => e.AssetMetaDataId).HasColumnName("AssetMetaDataID");

                entity.Property(e => e.AssetCaption)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AssetId).HasColumnName("AssetID");

                entity.Property(e => e.AssetTags)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.Asset)
                    .WithMany(p => p.AssetMetadata)
                    .HasForeignKey(d => d.AssetId)
                    .HasConstraintName("FK_AssetMetadata_Parent");
            });

            modelBuilder.Entity<AssetVariant>(entity =>
            {
                entity.ToTable("AssetVariant");

                entity.Property(e => e.AssetVariantId).HasColumnName("AssetVariantID");

                entity.Property(e => e.AssetId).HasColumnName("AssetID");

                entity.Property(e => e.AssetVariantName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BlobStoragePath)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.Asset)
                    .WithMany(p => p.AssetVariants)
                    .HasForeignKey(d => d.AssetId)
                    .HasConstraintName("FK_AssetVariant_Parent");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
