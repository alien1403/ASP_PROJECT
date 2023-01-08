using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Diagnostics;
using Taskmanager.Data;
using Taskmanager.Models;

namespace Taskmanager.Controllers
{


    public class DashboardController : Controller
    {

        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;


        public DashboardController(ApplicationDbContext ctx, UserManager<ApplicationUser> uM, RoleManager<IdentityRole> rM)
        {
            db = ctx;
            userManager = uM;
            roleManager = rM;
        }
        [Authorize(Roles = "User,Editor,Admin")]
        public IActionResult Index()
        {
            var id_user = userManager.GetUserId(User);

            var teams_p = from m in db.Members
                          where m.IdMember == id_user
                          join t in db.Teams
                          on m.IdTeam equals t.Id
                          select t;

            var teams_c = from t in db.Teams
                          where t.IdAdmin == id_user
                          select t;

            var project = from m in db.Members
                          where m.IdMember == id_user
                          join p in db.Projects
                          on m.IdTeam equals p.IdTeam
                          select p;

            var tasks = from tsk in db.Tasks
                        join tm in (from tmm in db.Task_Members
                                    where tmm.IdMember == id_user
                                    select tmm)
                        on tsk.Id equals tm.IdTask
                        select tsk;

            ViewBag.Tasks = tasks;
            ViewBag.Teams_C = teams_c;
            ViewBag.Teams_P = teams_p;
            ViewBag.Project_P = project;      
            return View();
        }
        [Authorize(Roles = "Admin")]
        public IActionResult dotnetMyAdmin()
        {
            List<Models.Team> teams = (from tm in db.Teams
                                      select tm).ToList<Models.Team>();

            return View(teams);
        }
    }
}
