﻿// <auto-generated />
using System;
using Domin.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Domin.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240819060210_TeachIn")]
    partial class TeachIn
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domin.Models.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Hours")
                        .HasColumnType("int");

                    b.Property<int>("LevelId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("LevelId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("Domin.Models.CourseDepartment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CoursesId")
                        .HasColumnType("int");

                    b.Property<int>("DepartmentsId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CoursesId");

                    b.HasIndex("DepartmentsId", "CoursesId")
                        .IsUnique();

                    b.ToTable("CourseDepartment");
                });

            modelBuilder.Entity("Domin.Models.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("Domin.Models.Enrollment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CoursesId")
                        .HasColumnType("int");

                    b.Property<decimal>("Grade")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("StudentsId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CoursesId");

                    b.HasIndex("StudentsId", "CoursesId")
                        .IsUnique();

                    b.ToTable("Enrollment");
                });

            modelBuilder.Entity("Domin.Models.Level", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Levels");
                });

            modelBuilder.Entity("Domin.Models.LevelDepartment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DepartmentsId")
                        .HasColumnType("int");

                    b.Property<int>("LevelId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LevelId");

                    b.HasIndex("DepartmentsId", "LevelId")
                        .IsUnique();

                    b.ToTable("LevelDepartment");
                });

            modelBuilder.Entity("Domin.Models.Professor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Salary")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Professors");
                });

            modelBuilder.Entity("Domin.Models.Section", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("Name", "DepartmentId")
                        .IsUnique();

                    b.ToTable("Section");
                });

            modelBuilder.Entity("Domin.Models.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<int>("DepId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("Gpa")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("LevelId")
                        .HasColumnType("int");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SectionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DepId");

                    b.HasIndex("LevelId");

                    b.HasIndex("SectionId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("Domin.Models.TeachBy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<int>("ProfessorId")
                        .HasColumnType("int");

                    b.Property<int>("SectionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("ProfessorId");

                    b.HasIndex("SectionId", "CourseId")
                        .IsUnique();

                    b.ToTable("TeachBy");
                });

            modelBuilder.Entity("Domin.Models.TeachIn", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<int>("ProfessorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("ProfessorId", "DepartmentId")
                        .IsUnique();

                    b.ToTable("TeachIn");
                });

            modelBuilder.Entity("Domin.Models.TeachTo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CoursesId")
                        .HasColumnType("int");

                    b.Property<int>("ProfessorsId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CoursesId");

                    b.HasIndex("ProfessorsId", "CoursesId")
                        .IsUnique();

                    b.ToTable("TeachTo");
                });

            modelBuilder.Entity("Domin.Models.Course", b =>
                {
                    b.HasOne("Domin.Models.Level", "Level")
                        .WithMany("courses")
                        .HasForeignKey("LevelId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Level");
                });

            modelBuilder.Entity("Domin.Models.CourseDepartment", b =>
                {
                    b.HasOne("Domin.Models.Course", "Course")
                        .WithMany("departments")
                        .HasForeignKey("CoursesId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Domin.Models.Department", "Department")
                        .WithMany("courses")
                        .HasForeignKey("DepartmentsId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Department");
                });

            modelBuilder.Entity("Domin.Models.Enrollment", b =>
                {
                    b.HasOne("Domin.Models.Course", "Course")
                        .WithMany("Students")
                        .HasForeignKey("CoursesId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Domin.Models.Student", "Student")
                        .WithMany("courses")
                        .HasForeignKey("StudentsId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Domin.Models.LevelDepartment", b =>
                {
                    b.HasOne("Domin.Models.Department", "Department")
                        .WithMany("levels")
                        .HasForeignKey("DepartmentsId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Domin.Models.Level", "Level")
                        .WithMany("Departments")
                        .HasForeignKey("LevelId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Department");

                    b.Navigation("Level");
                });

            modelBuilder.Entity("Domin.Models.Section", b =>
                {
                    b.HasOne("Domin.Models.Department", "Department")
                        .WithMany("sections")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("Domin.Models.Student", b =>
                {
                    b.HasOne("Domin.Models.Department", "Department")
                        .WithMany("Students")
                        .HasForeignKey("DepId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Domin.Models.Level", "Level")
                        .WithMany("students")
                        .HasForeignKey("LevelId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Domin.Models.Section", "section")
                        .WithMany("Students")
                        .HasForeignKey("SectionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Department");

                    b.Navigation("Level");

                    b.Navigation("section");
                });

            modelBuilder.Entity("Domin.Models.TeachBy", b =>
                {
                    b.HasOne("Domin.Models.Course", "Course")
                        .WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Domin.Models.Professor", "Professor")
                        .WithMany("Sections")
                        .HasForeignKey("ProfessorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Domin.Models.Section", "Section")
                        .WithMany("Professors")
                        .HasForeignKey("SectionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Professor");

                    b.Navigation("Section");
                });

            modelBuilder.Entity("Domin.Models.TeachIn", b =>
                {
                    b.HasOne("Domin.Models.Department", "Department")
                        .WithMany("Professors")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Domin.Models.Professor", "Professor")
                        .WithMany("Departments")
                        .HasForeignKey("ProfessorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Department");

                    b.Navigation("Professor");
                });

            modelBuilder.Entity("Domin.Models.TeachTo", b =>
                {
                    b.HasOne("Domin.Models.Course", "Course")
                        .WithMany("professors")
                        .HasForeignKey("CoursesId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Domin.Models.Professor", "Professor")
                        .WithMany("courses")
                        .HasForeignKey("ProfessorsId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Professor");
                });

            modelBuilder.Entity("Domin.Models.Course", b =>
                {
                    b.Navigation("Students");

                    b.Navigation("departments");

                    b.Navigation("professors");
                });

            modelBuilder.Entity("Domin.Models.Department", b =>
                {
                    b.Navigation("Professors");

                    b.Navigation("Students");

                    b.Navigation("courses");

                    b.Navigation("levels");

                    b.Navigation("sections");
                });

            modelBuilder.Entity("Domin.Models.Level", b =>
                {
                    b.Navigation("Departments");

                    b.Navigation("courses");

                    b.Navigation("students");
                });

            modelBuilder.Entity("Domin.Models.Professor", b =>
                {
                    b.Navigation("Departments");

                    b.Navigation("Sections");

                    b.Navigation("courses");
                });

            modelBuilder.Entity("Domin.Models.Section", b =>
                {
                    b.Navigation("Professors");

                    b.Navigation("Students");
                });

            modelBuilder.Entity("Domin.Models.Student", b =>
                {
                    b.Navigation("courses");
                });
#pragma warning restore 612, 618
        }
    }
}
