using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using Taskmanager.Data.Migrations;
using Taskmanager.Models;

namespace Taskmanager.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
          protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Member>()
                .HasKey(x => new {x.IdMember, x.IdTeam});

            builder.Entity<Models.Task_Member>()
                .HasKey(x => new { x.IdMember, x.IdTask });

            builder.Entity<Models.Task>().HasMany(t => t.Comments).WithOne(c => c.Task).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Models.Task>().HasMany(t => t.TaskMembers).WithMany(tm => tm.Tasks)
     .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Models.Projects>().HasMany(t => t.Tasks).WithOne(p => p.Project).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Models.Team>().HasMany(p => p.Projects).WithOne(t => t.Team).OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(builder);
        }

        public DbSet<ApplicationUser> Users { get; set; }  
        public DbSet<Models.Task> Tasks { get; set; }
        public DbSet<Models.Team> Teams { get; set; }
        public DbSet<Models.Status> Statuses { get; set; }
        public DbSet<Models.Comment> Comments { get; set; }
        public DbSet<Models.Projects> Projects { get; set; }
        public DbSet<Models.Member> Members { get; set; }
        public DbSet<Models.Task_Member> Task_Members { get; set; }


    }
}