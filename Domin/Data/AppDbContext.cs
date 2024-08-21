using Domin.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Domin.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
       
        // DbSets
        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseDepartment> CourseDepartment { get; set; }
      
        public DbSet<Level> Levels { get; set; }
        public DbSet<Professor> Professors { get; set; }
        public DbSet<Section> Section { get; set; }
        public DbSet<TeachTo> TeachTo { get; set; }
        public DbSet<Enrollment> Enrollment { get; set; }
     
        public DbSet<TeachBy> TeachBy { get; set; }
        public DbSet<TeachIn> TeachIn { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            ////One To Many
            ////Student
            //modelBuilder.Entity<Student>().HasOne(d => d.Department).WithMany(s => s.students).OnDelete(DeleteBehavior.Restrict);
            //modelBuilder.Entity<Student>().HasOne(d => d.Level).WithMany(s => s.students).OnDelete(DeleteBehavior.Restrict);
            ////Courses
            //modelBuilder.Entity<Course>().HasOne(d => d.Level).WithMany(c=> c.courses).OnDelete(DeleteBehavior.Restrict);

            ////Many TO Many
            //modelBuilder.Entity<Enrollment>().HasOne(s=>s.Student).WithMany(c=>c.courses).OnDelete(DeleteBehavior.Restrict);
            //modelBuilder.Entity<Enrollment>().HasOne(c=>c.Course).WithMany(s=>s.students).OnDelete(DeleteBehavior.Restrict);
            //modelBuilder.Entity<TeachTo>().HasOne(p => p.Professor).WithMany(c => c.courses).OnDelete(DeleteBehavior.Restrict);
            //modelBuilder.Entity<TeachTo>().HasOne(c => c.Course).WithMany(p => p.professors).OnDelete(DeleteBehavior.Restrict);
            //modelBuilder.Entity<TeachIn>().HasOne(p => p.Professor).WithMany(D => D.depatrment).OnDelete(DeleteBehavior.Restrict);
            //modelBuilder.Entity<TeachIn>().HasOne(D => D.Department).WithMany(p => p.professors).OnDelete(DeleteBehavior.Restrict);
            //modelBuilder.Entity<TeachBy>().HasOne(p => p.professor).WithMany(s => s.student).OnDelete(DeleteBehavior.Restrict);
            ////modelBuilder.Entity<TeachBy>().HasOne(s => s.Student).WithMany(p => p.professors).OnDelete(DeleteBehavior.Restrict);


            //modelBuilder.Entity<CourseDepartment>().HasOne(c => c.Course).WithMany(d => d.departments).OnDelete(DeleteBehavior.Restrict);
            //modelBuilder.Entity<CourseDepartment>().HasOne(d => d.Department).WithMany(c => c.courses).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TeachTo>()
              .HasIndex(t => new { t.ProfessorsId, t.CoursesId })
              .IsUnique();
            modelBuilder.Entity<TeachBy>()
              .HasIndex(t => new { t.SectionId, t.CourseId })
              .IsUnique();
            modelBuilder.Entity<Enrollment>()
            .HasIndex(t => new { t.StudentsId, t.CoursesId })
            .IsUnique(); 
                 modelBuilder.Entity<CourseDepartment>()
            .HasIndex(t => new { t.DepartmentsId, t.CoursesId })
            .IsUnique();
            modelBuilder.Entity<LevelDepartment>()
             .HasIndex(t => new { t.DepartmentsId, t.LevelId })
             .IsUnique();

            modelBuilder.Entity<Section>()
             .HasIndex(t => new { t.Name, t.DepartmentId , t.LevelId})

             .IsUnique();
            modelBuilder.Entity<TeachIn>()
            .HasIndex(t => new { t.ProfessorId, t.DepartmentId })
            .IsUnique();



            foreach (var relation in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {

                relation.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

    }

}
