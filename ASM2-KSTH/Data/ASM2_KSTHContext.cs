using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ASM2_KSTH.Models;
using ASM2_KSTH.ViewModels;

namespace ASM2_KSTH.Data
{
    public class ASM2_KSTHContext : DbContext
    {
        public ASM2_KSTHContext(DbContextOptions<ASM2_KSTHContext> options)
            : base(options)
        {
        }
        public DbSet<Major> Majors { get; set; }
        public DbSet<Admin> Admins {  get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Roles> Roles { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Major>()
                .HasMany(m => m.Students)
                .WithOne(s => s.Major)
                .HasForeignKey(s => s.MajorId);

            modelBuilder.Entity<Major>()
                .HasMany(m => m.Courses)
                .WithOne(c => c.Major)
                .HasForeignKey(c => c.MajorId);

            modelBuilder.Entity<Course>()
                .HasMany(c => c.Classes)
                .WithOne(cl => cl.Course)
                .HasForeignKey(cl => cl.CourseId);

            modelBuilder.Entity<Class>()
                .HasMany(cl => cl.Enrollments)
                .WithOne(e => e.Class)
                .HasForeignKey(e => e.ClassId);

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Student)
                .WithMany(s => s.Enrollments)
                .HasForeignKey(e => e.StudentId)
                .OnDelete(DeleteBehavior.NoAction);
            // Cấu hình bảng Roles
            modelBuilder.Entity<Roles>(entity =>
            {
                entity.HasKey(e => e.RoleId);
                entity.Property(e => e.RoleName)
                    .HasMaxLength(100)
                    .IsRequired();
            });

            base.OnModelCreating(modelBuilder);

            // Cấu hình bảng Student
            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.StudentId);
                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsRequired(false);

                // Cấu hình khóa ngoại
                entity.HasOne(e => e.Roles)
                    .WithMany()
                    .HasForeignKey(e => e.RoleId)
                    .IsRequired();

                entity.HasOne(e => e.Major)
				    .WithMany(m => m.Students)
					.HasForeignKey(s => s.MajorId)
                    .IsRequired();
			});

			// Cấu hình bảng Teacher
			modelBuilder.Entity<Teacher>(entity =>
            {
                entity.HasKey(e => e.TeacherId);
                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsRequired();

                // Cấu hình khóa ngoại
                entity.HasOne(e => e.Roles)
                    .WithMany()
                    .HasForeignKey(e => e.RoleId)
                    .IsRequired();
            });

            // Cấu hình bảng Admin
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Username)
                    .HasMaxLength(100)
                    .IsRequired();

                // Cấu hình khóa ngoại
                entity.HasOne(e => e.Roles)
                    .WithMany()
                    .HasForeignKey(e => e.RoleId)
                    .IsRequired();
            });
        }

    }

}
    

