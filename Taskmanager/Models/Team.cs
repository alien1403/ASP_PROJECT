using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Taskmanager.Models
{
    public class Team
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("IdentityUser")]
        public string? IdAdmin{ get; set; }

        [Required(ErrorMessage = "The Team name is required")]
        [MaxLength(50, ErrorMessage = "Team name is too long(max 50)")]
        [MinLength(3, ErrorMessage = "Team name is too short(min 3)")]

        public string? Name { get; set; }

        public virtual IEnumerable<Member>? Members { get; set; }
       
    }
}
