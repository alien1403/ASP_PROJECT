using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Taskmanager.Models
{
    // public class EFContext : DbContext
    //{
    //   protected override void OnModelCreating(ModelBuilder modelbuild)
    //    {
    //        modelbuild.Entity<Member>().HasKey(e => new { e.Id, e.IdMember, e.IdTeam });
   //      }

//}
    public class Member
    {
         [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        
        public string? IdMember { get; set; }
       
        public int? IdTeam { get; set; }
    

    }
}
