using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        public DbSet<Project> Projects { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<StudentNote>().HasOne(x => x.Teacher).WithMany().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<StudentNote>().HasOne(x => x.Student).WithMany().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<StudentProject>()
                .HasKey(x => new { x.StudentId, x.ProjectId });

        }

    }

}