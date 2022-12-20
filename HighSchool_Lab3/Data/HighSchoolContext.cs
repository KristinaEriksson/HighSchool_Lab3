using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using HighSchool_Lab3.Models;

namespace HighSchool_Lab3.Data
{
    public partial class HighSchoolContext : DbContext
    {
        public HighSchoolContext()
        {
        }

        public HighSchoolContext(DbContextOptions<HighSchoolContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Grade> Grades { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source = DESKTOP-8VGM57M; Initial Catalog = HighSchool3.0; Integrated Security=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.EmploymentNumber);

                entity.ToTable("Employee");

                entity.Property(e => e.EmploymentNumber).ValueGeneratedNever();

                entity.Property(e => e.EmploymentDate).HasColumnType("date");

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.Salary).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.SocialSecurityNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Title).HasMaxLength(50);
            });

            modelBuilder.Entity<Grade>(entity =>
            {
                entity.ToTable("Grade");

                entity.Property(e => e.EmployeeName).HasMaxLength(50);

                entity.Property(e => e.FkEmploymentNumber).HasColumnName("FK_EmploymentNumber");

                entity.Property(e => e.FkStudentId).HasColumnName("FK_StudentId");

                entity.Property(e => e.Grade1)
                    .HasMaxLength(1)
                    .HasColumnName("Grade");

                entity.Property(e => e.SetDate).HasColumnType("date");

                entity.Property(e => e.StudentName).HasMaxLength(50);

                entity.Property(e => e.Subject).HasMaxLength(50);

                entity.HasOne(d => d.FkEmploymentNumberNavigation)
                    .WithMany(p => p.Grades)
                    .HasForeignKey(d => d.FkEmploymentNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Grade_Employee");

                entity.HasOne(d => d.FkStudent)
                    .WithMany(p => p.Grades)
                    .HasForeignKey(d => d.FkStudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Grade_Student");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Student");

                entity.Property(e => e.Class).HasMaxLength(50);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.SocialSecurityNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
