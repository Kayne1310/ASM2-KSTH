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
        public object Schedule { get; internal set; }

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


            modelBuilder.Entity<Schedule>()
            .HasOne(s => s.Class)
            .WithMany()
            .HasForeignKey(s => s.ClassId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Schedule>()
                .HasOne(s => s.Room)
                .WithMany()
                .HasForeignKey(s => s.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Schedule>()
                .HasOne(s => s.Course)
                .WithMany()
                .HasForeignKey(s => s.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

        }
        public DbSet<ASM2_KSTH.Models.Schedule> Schedule_1 { get; set; } = default!;
        

    }

}
    

