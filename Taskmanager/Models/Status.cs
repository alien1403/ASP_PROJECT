using System.ComponentModel.DataAnnotations;

namespace Taskmanager.Models
{
    public class Status
    {
        [Key]
        public int? Id { get; set; }
        [Required(ErrorMessage = "The StatusName is required")]
        public string? StatusName { get; set; }

        public virtual ICollection<Task>? Tasks{ get; set; }
    }
}
