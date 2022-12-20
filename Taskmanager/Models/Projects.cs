using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Taskmanager.Models
{
    public class Projects
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("IdentityUser")]
        public string? IdAdmin { get; set; }

        [ForeignKey("Team")]
        public int IdTeam { get; set; }

        [Required(ErrorMessage = "The Project name is required")]
        [MaxLength(50,ErrorMessage = "Project name is too long")]
        [MinLength(5,ErrorMessage ="Project name is too short")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "The description is required")]
        [MaxLength(100, ErrorMessage = "Description is too long")]
        [MinLength(5, ErrorMessage = "Description is too short")]
        public string? Description { get; set; }

        public virtual ApplicationUser? User { get; set; }
        public virtual Team? Team { get; set; }
        
    }
}
