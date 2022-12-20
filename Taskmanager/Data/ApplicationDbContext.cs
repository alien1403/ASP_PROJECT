using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
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
        }

        public DbSet<ApplicationUser> Users { get; set; }  
        public DbSet<Models.Task> Tasks { get; set; }
        public DbSet<Models.Team> Teams { get; set; }
        public DbSet<Models.Status> Statuses { get; set; }
        public DbSet<Models.Comment> Comments { get; set; }
        public DbSet<Models.Projects> Projects { get; set; }
        public DbSet<Models.Member> Members { get; set; }


    }
}