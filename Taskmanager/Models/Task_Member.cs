namespace Taskmanager.Models
{
    public class Task_Member
    {
        public string IdMember { get; set; }
        public int IdTask { get; set; }

        public virtual ICollection<Models.Task> Tasks { get; set; }
        public virtual ICollection<Models.Member> Members { get; set; }
    }
}
