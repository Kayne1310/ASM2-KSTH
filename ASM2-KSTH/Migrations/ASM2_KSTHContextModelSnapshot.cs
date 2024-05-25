﻿// <auto-generated />
using System;
using ASM2_KSTH.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ASM2_KSTH.Migrations
{
    [DbContext(typeof(ASM2_KSTHContext))]
    partial class ASM2_KSTHContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ASM2_KSTH.Models.Class", b =>
            {
                b.Property<int>("ClassId")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClassId"));

                b.Property<int>("CourseId")
                    .HasColumnType("int");

                b.Property<int>("RoomId")
                    .HasColumnType("int");

                b.Property<string>("Semester")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<int>("TeacherId")
                    .HasColumnType("int");

                b.Property<int>("Year")
                    .HasColumnType("int");

                b.HasKey("ClassId");

                b.HasIndex("CourseId");

                b.HasIndex("RoomId");

                b.HasIndex("TeacherId");

                b.ToTable("Classes");
            });

            modelBuilder.Entity("ASM2_KSTH.Models.Course", b =>
            {
                b.Property<int>("CourseId")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CourseId"));

                b.Property<string>("CourseDescription")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("CourseName")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<int>("Credits")
                    .HasColumnType("int");

                b.Property<int>("MajorId")
                    .HasColumnType("int");

                b.HasKey("CourseId");

                b.HasIndex("MajorId");

                b.ToTable("Courses");
            });

            modelBuilder.Entity("ASM2_KSTH.Models.Enrollment", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                b.Property<int>("ClassId")
                    .HasColumnType("int");

                b.Property<int>("StudentId")
                    .HasColumnType("int");

                b.HasKey("Id");

                b.HasIndex("ClassId");

                b.HasIndex("StudentId");

                b.ToTable("Enrollments");
            });

            modelBuilder.Entity("ASM2_KSTH.Models.Grade", b =>
            {
                b.Property<int>("GradeId")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GradeId"));

                b.Property<int>("CourseId")
                    .HasColumnType("int");

                b.Property<int>("EnrollmentId")
                    .HasColumnType("int");

                b.Property<DateTime>("GradeDate")
                    .HasColumnType("datetime2");

                b.Property<string>("GradeValue")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.HasKey("GradeId");

                b.HasIndex("CourseId");

                b.HasIndex("EnrollmentId");

                b.ToTable("Grades");
            });

            modelBuilder.Entity("ASM2_KSTH.Models.Major", b =>
            {
                b.Property<int>("MajorId")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MajorId"));

                b.Property<string>("MajorName")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.HasKey("MajorId");

                b.ToTable("Majors");
            });

            modelBuilder.Entity("ASM2_KSTH.Models.Room", b =>
            {
                b.Property<int>("RoomId")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoomId"));

                b.Property<string>("RoomNumber")
                    .HasColumnType("nvarchar(max)");

                b.HasKey("RoomId");

                b.ToTable("Rooms");
            });

            modelBuilder.Entity("ASM2_KSTH.Models.Student", b =>
            {
                b.Property<int>("StudentId")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StudentId"));

                b.Property<string>("Address")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<DateTime>("DateOfBirth")
                    .HasColumnType("datetime2");

                b.Property<string>("Email")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<int>("MajorId")
                    .HasColumnType("int");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Password")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("PhoneNumber")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Username")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.HasKey("StudentId");

                b.HasIndex("MajorId");

                b.ToTable("Students");
            });

            modelBuilder.Entity("ASM2_KSTH.Models.Teacher", b =>
            {
                b.Property<int>("TeacherId")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TeacherId"));

                b.Property<string>("Department")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Email")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Password")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("PhoneNumber")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Username")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.HasKey("TeacherId");

                b.ToTable("Teachers");
            });

            modelBuilder.Entity("ASM2_KSTH.Models.Test", b =>
            {
                b.Property<int>("id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                b.Property<string>("Ten")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Title")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.HasKey("id");

                b.ToTable("Tests");
            });

            modelBuilder.Entity("ASM2_KSTH.Models.Class", b =>
            {
                b.HasOne("ASM2_KSTH.Models.Course", "Course")
                    .WithMany("Classes")
                    .HasForeignKey("CourseId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("ASM2_KSTH.Models.Room", "Room")
                    .WithMany("Classes")
                    .HasForeignKey("RoomId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("ASM2_KSTH.Models.Teacher", "Teacher")
                    .WithMany("Classes")
                    .HasForeignKey("TeacherId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Course");

                b.Navigation("Room");

                b.Navigation("Teacher");
            });

            modelBuilder.Entity("ASM2_KSTH.Models.Course", b =>
            {
                b.HasOne("ASM2_KSTH.Models.Major", "Major")
                    .WithMany("Courses")
                    .HasForeignKey("MajorId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Major");
            });

            modelBuilder.Entity("ASM2_KSTH.Models.Enrollment", b =>
            {
                b.HasOne("ASM2_KSTH.Models.Class", "Class")
                    .WithMany("Enrollments")
                    .HasForeignKey("ClassId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("ASM2_KSTH.Models.Student", "Student")
                    .WithMany("Enrollments")
                    .HasForeignKey("StudentId")
                    .OnDelete(DeleteBehavior.NoAction)
                    .IsRequired();

                b.Navigation("Class");

                b.Navigation("Student");
            });

            modelBuilder.Entity("ASM2_KSTH.Models.Grade", b =>
            {
                b.HasOne("ASM2_KSTH.Models.Course", "Course")
                    .WithMany()
                    .HasForeignKey("CourseId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("ASM2_KSTH.Models.Enrollment", "Enrollment")
                    .WithMany("Grades")
                    .HasForeignKey("EnrollmentId")
                    .OnDelete(DeleteBehavior.NoAction)
                    .IsRequired();

                b.Navigation("Course");

                b.Navigation("Enrollment");
            });

            modelBuilder.Entity("ASM2_KSTH.Models.Student", b =>
            {
                b.HasOne("ASM2_KSTH.Models.Major", "Major")
                    .WithMany("Students")
                    .HasForeignKey("MajorId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Major");
            });

            modelBuilder.Entity("ASM2_KSTH.Models.Class", b =>
            {
                b.Navigation("Enrollments");
            });

            modelBuilder.Entity("ASM2_KSTH.Models.Course", b =>
            {
                b.Navigation("Classes");
            });

            modelBuilder.Entity("ASM2_KSTH.Models.Enrollment", b =>
            {
                b.Navigation("Grades");
            });

            modelBuilder.Entity("ASM2_KSTH.Models.Major", b =>
            {
                b.Navigation("Courses");

                b.Navigation("Students");
            });

            modelBuilder.Entity("ASM2_KSTH.Models.Room", b =>
            {
                b.Navigation("Classes");
            });

            modelBuilder.Entity("ASM2_KSTH.Models.Student", b =>
            {
                b.Navigation("Enrollments");
            });

            modelBuilder.Entity("ASM2_KSTH.Models.Teacher", b =>
            {
                b.Navigation("Classes");
            });
#pragma warning restore 612, 618
        }
    }
}
