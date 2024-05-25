using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ASM2_KSTH.Models;

namespace ASM2_KSTH.Data
{
    public class ASM2_KSTHContext : DbContext
    {
        public ASM2_KSTHContext(DbContextOptions<ASM2_KSTHContext> options)
            : base(options)
        {
        }

        public DbSet<ASM2_KSTH.Models.Admin> Ladmin { get; set; } = default!;
        public DbSet<Major> Majors { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Room> Rooms { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-T5I26HK;Initial Catalog=KSTH;Integrated Security=True;Trust Server Certificate=True");
        }
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

            modelBuilder.Entity<Teacher>()
                .HasMany(t => t.Classes)
                .WithOne(cl => cl.Teacher)
                .HasForeignKey(cl => cl.TeacherId);


       

        }
    }
    
}
