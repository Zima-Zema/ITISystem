﻿using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ITISystem.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Instructor> Instructor { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Dept_Crs_Instr> DeptS_CrS_InstrS { get; set; }
        public virtual DbSet<Std_Crs_Instr> StdS_CrS_InstrS { get; set; }
        public virtual DbSet<Exam> Exams { get; set; }
        public virtual DbSet<Permisions> Premissions { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Attendance> Attendances { get; set; }
        public virtual DbSet<Std_Exam> StdS_ExamS { get; set; }
        public virtual DbSet<Std_Exam_Quest> StdS_ExamS_QuestS { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}