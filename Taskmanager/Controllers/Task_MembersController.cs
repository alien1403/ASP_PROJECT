using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Taskmanager.Data;
using Taskmanager.Models;

namespace Taskmanager.Controllers
{

    public class Task_MembersController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public Task_MembersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            db = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public IActionResult Add(int id)
        {
            var t = from p in db.Projects
                    where p.Id == (from t in db.Tasks
                                  where t.Id == id
                                  select t.IdProject).First()
                    select p.IdTeam;
            
            var members = from m in db.Members
                          where m.IdTeam == t.First()
                          select m.IdMember;

            List<ApplicationUser> users = new();

            foreach (var i in members)
            {
                if (db.Task_Members.Find(i, id) == null)
                {
                    users.Add(db.Users.Find(i));
                }
            }
            ViewBag.Id = id;

            return View(users);
        }
        [HttpPost]
        public IActionResult Add(List<string> m_id, int id) {

          

            foreach(var e in m_id)
            {
                db.Task_Members.Add(new Task_Member() { IdMember = e,IdTask = id  }) ;
            }
            db.SaveChanges();

            return RedirectToAction("Show", "Tasks", new {id = id});
        }
        [HttpPost]
        public IActionResult Delete(string m_id, int t_id)
        {
            db.Task_Members.Remove(db.Task_Members.Find(m_id, t_id));
            db.SaveChanges(true);
            return RedirectToAction("Show", "Tasks", new { id = t_id });
        }
    }
}
