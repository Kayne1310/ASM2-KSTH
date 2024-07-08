using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ASM2_KSTH.Models;

public partial class KsthContext : DbContext
{
    public KsthContext()
    {
    }

    public KsthContext(DbContextOptions<KsthContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Attendance> Attendance { get; set; }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Enrollment> Enrollments { get; set; }

    public virtual DbSet<Grade> Grades { get; set; }

    public virtual DbSet<Major> Majors { get; set; }

    public virtual DbSet<NumSession> NumSessions { get; set; }

    public virtual DbSet<Roles> Roles { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }


    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Ladmin");

            entity.Property(e => e.Password).HasDefaultValue("");
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .IsUnicode(false)
                .UseCollation("SQL_Latin1_General_CP1_CS_AS");

           
        });

        modelBuilder.Entity<Attendance>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Attendan__3214EC277EF2579A");

            entity.ToTable("Attendance");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.AttendanceStatus)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ClassId).HasColumnName("ClassID");
            entity.Property(e => e.EnrollmentId).HasColumnName("EnrollmentID");
            entity.Property(e => e.Reason)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.RoomId).HasColumnName("RoomID");
            entity.Property(e => e.StudentId).HasColumnName("StudentID");
            entity.Property(e => e.TeacherId).HasColumnName("TeacherID");

            entity.HasOne(d => d.Class).WithMany(p => p.Attendance)
                .HasForeignKey(d => d.ClassId)
                .HasConstraintName("FK__Attendanc__Class__160F4887");

            entity.HasOne(d => d.Enrollment).WithMany(p => p.Attendance)
                .HasForeignKey(d => d.EnrollmentId)
                .HasConstraintName("FK__Attendanc__Enrol__17036CC0");

            entity.HasOne(d => d.Room).WithMany(p => p.Attendance)
                .HasForeignKey(d => d.RoomId)
                .HasConstraintName("FK__Attendanc__RoomI__17F790F9");

            entity.HasOne(d => d.Student).WithMany(p => p.Attendance)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK__Attendanc__Stude__18EBB532");

            entity.HasOne(d => d.Teacher).WithMany(p => p.Attendance)
                .HasForeignKey(d => d.TeacherId)
                .HasConstraintName("FK__Attendanc__Teach__19DFD96B");
        });

        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.ClassId).HasName("PK__Classes__CB1927C0A7311077");

            entity.Property(e => e.ClassId).ValueGeneratedNever();
            entity.Property(e => e.ClassName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Semester)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.Course).WithMany(p => p.Classes)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK__Classes__CourseI__48CFD27E");

            entity.HasOne(d => d.Room).WithMany(p => p.Classes)
                .HasForeignKey(d => d.RoomId)
                .HasConstraintName("FK__Classes__RoomId__4AB81AF0");

        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("PK__Courses__C92D71A72753799D");

            entity.Property(e => e.CourseId).ValueGeneratedNever();
            entity.Property(e => e.CourseDescription).HasColumnType("text");
            entity.Property(e => e.CourseName)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Major).WithMany(p => p.Courses)
                .HasForeignKey(d => d.MajorId)
                .HasConstraintName("FK__Courses__MajorId__45F365D3");
        });

        modelBuilder.Entity<Enrollment>(entity =>
        {
            entity.HasKey(e => e.EnrollmentId).HasName("PK__Enrollme__3214EC07F1363D14");

            entity.Property(e => e.EnrollmentId).ValueGeneratedNever();

            entity.HasOne(d => d.Class).WithMany(p => p.Enrollments)
                .HasForeignKey(d => d.ClassId)
                .HasConstraintName("FK__Enrollmen__Class__52593CB8");

            entity.HasOne(d => d.Student).WithMany(p => p.Enrollments)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK__Enrollmen__Stude__5165187F");
        });

        modelBuilder.Entity<Grade>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.Grade1).HasColumnType("decimal(3, 2)");
            entity.Property(e => e.GradeId).ValueGeneratedOnAdd();

            entity.HasOne(d => d.Course).WithMany()
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Grades_Courses");

            entity.HasOne(d => d.Enrollment).WithMany()
                .HasForeignKey(d => d.EnrollmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Grades_Enrollments");
        });

        modelBuilder.Entity<NumSession>(entity =>
        {
            entity.HasKey(e => e.NumId).HasName("PK_Num_Session");

            entity.ToTable("NumSessions");

            entity.Property(e => e.Numses)
                  .HasMaxLength(255)
                  .IsUnicode(false);

            entity.HasOne(d => d.Course)
                  .WithMany(p => p.NumSessions)
                  .HasForeignKey(d => d.CourseId)
                  .HasConstraintName("FK_Num_Session_Courses");
        });

        modelBuilder.Entity<Roles>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE1AAEDC9665");

            entity.Property(e => e.RoleId).ValueGeneratedNever();
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK_Lstudent");

            entity.HasIndex(e => e.MajorId, "IX_Lstudent_MajorId");

            entity.Property(e => e.RandomKey).HasMaxLength(55);
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasDefaultValue("");

            entity.HasOne(d => d.Major).WithMany(p => p.Students)
                .HasForeignKey(d => d.MajorId)
                .HasConstraintName("FK_Lstudent_Majors_MajorId");

           
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.HasKey(e => e.TeacherId).HasName("PK_Lteacher");

            entity.Property(e => e.Password).HasDefaultValue("");
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasDefaultValue("");

           
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
