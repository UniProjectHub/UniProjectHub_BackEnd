﻿using Domain.Models;
using Infracstructures.FluentAPIs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Infracstructures
{
    public class AppDbContext : IdentityDbContext<Users>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public AppDbContext()
        {
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Domain.Models.Task> Tasks { get; set; } // Ensure this is added

        public DbSet<Member> Members { get; set; }
        public DbSet<GroupChat> GroupChats { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Domain.Models.File> Files { get; set; }
         public DbSet<MemberInTask> MemberInTasks { get; set; }
        public DbSet<Notification> Notifications { get; set; }
         public DbSet<Project> Projects { get; set; }
         public DbSet<SubTask> SubTasks { get; set; }
         protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure primary key for IdentityUserLogin<string>
            builder.Entity<IdentityUserLogin<string>>().HasKey(l => l.UserId);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                },
            };
            builder.Entity<IdentityRole>().HasData(roles);
            builder.ApplyConfiguration(new ScheduleConfiguration());
         }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}
