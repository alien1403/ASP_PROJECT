using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Taskmanager.Models
{
    public class Task
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Project")]
        public int? IdProject;
        
        [MinLength(5, ErrorMessage = "Title is too short.(min 5)")]
        [MaxLength (50, ErrorMessage = "Title is too long.(max 50)")]
        [Required(ErrorMessage = "The title is required")]
        public string? Title { get; set; }

        [MinLength(5, ErrorMessage = "Description is too short.(min 5)")]
        [MaxLength(100, ErrorMessage = "Decription is too long.(max 50)")]

        [Required(ErrorMessage = "The Description is required")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "The StartDate is required.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "The EndDate is required")]
        public DateTime EndDate { get; set; }
        public int? StatusId { get; set; }
        public string? UserId { get; set; }

        public virtual ApplicationUser? User { get; set; }

        public virtual Status? Status { get; set; }
        public virtual Projects? Project { get; set; }

        public virtual ICollection<Comment>? Comments { get; set; }
        [NotMapped]
        public IEnumerable<SelectListItem>? Stat { get; set; }

    }
}
