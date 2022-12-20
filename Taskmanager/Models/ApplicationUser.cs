using Microsoft.AspNetCore.Identity;

namespace Taskmanager.Models
{
    public class ApplicationUser: IdentityUser
    {
        public virtual ICollection<Projects>? Projects { get; set; }
        public virtual ICollection<Comment>? Comments { get; set; }
    }
}
