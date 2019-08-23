using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ELearning.Models
{
    public partial class ELearningDBContext : DbContext
    {
        public ELearningDBContext()
        {
        }

        public ELearningDBContext(DbContextOptions<ELearningDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Author> Author { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Course> Course { get; set; }
        public virtual DbSet<CourseInfo> CourseInfo { get; set; }
        public virtual DbSet<CourseLevel> CourseLevel { get; set; }
        public virtual DbSet<CourseModule> CourseModule { get; set; }
        public virtual DbSet<SubCategory> SubCategory { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-HR3RL14\\EXPRESSSQL;Database=ELearningDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>(entity =>
            {
                entity.Property(e => e.AuthorId).HasColumnName("AuthorID");

                entity.Property(e => e.AuthorEmail)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.AuthorFirstName)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.AuthorImage)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.AuthorLastName)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.AuthorTelephone)
                    .HasMaxLength(256)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CategoryImage).HasMaxLength(500);

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.Property(e => e.CourseId).HasColumnName("CourseID");

                entity.Property(e => e.AuthorId).HasColumnName("AuthorID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CourseDescription)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.CourseImage)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.CourseName)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.SubCategoryId).HasColumnName("SubCategoryID");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Course)
                    .HasForeignKey(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Course__AuthorID__173876EA");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Course)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Course__Category__182C9B23");

                entity.HasOne(d => d.SubCategory)
                    .WithMany(p => p.Course)
                    .HasForeignKey(d => d.SubCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Course__SubCateg__1920BF5C");
            });

            modelBuilder.Entity<CourseInfo>(entity =>
            {
                entity.Property(e => e.CourseInfoId).HasColumnName("CourseInfoID");

                entity.Property(e => e.CourseDuration)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.CourseId).HasColumnName("CourseID");

                entity.Property(e => e.CourseLevelId).HasColumnName("CourseLevelID");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.CourseInfo)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CourseInf__Cours__1BFD2C07");

                entity.HasOne(d => d.CourseLevel)
                    .WithMany(p => p.CourseInfo)
                    .HasForeignKey(d => d.CourseLevelId)
                    .HasConstraintName("FK__CourseInf__Cours__239E4DCF");
            });

            modelBuilder.Entity<CourseLevel>(entity =>
            {
                entity.Property(e => e.CourseLevelId).HasColumnName("CourseLevelID");

                entity.Property(e => e.CourseLevelName)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CourseModule>(entity =>
            {
                entity.Property(e => e.CourseModuleId).HasColumnName("CourseModuleID");

                entity.Property(e => e.CourseId).HasColumnName("CourseID");

                entity.Property(e => e.CourseModuleName)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.ModuleDuration)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.ModuleVideo)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.CourseModule)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CourseMod__Cours__21B6055D");
            });

            modelBuilder.Entity<SubCategory>(entity =>
            {
                entity.Property(e => e.SubCategoryId).HasColumnName("SubCategoryID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.SubCategoryImage).HasMaxLength(500);

                entity.Property(e => e.SubCategoryName)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.SubCategory)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SubCatego__Categ__145C0A3F");
            });
        }
    }
}
