using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
            ViewBag.Teams_C = teams_c;
            ViewBag.Teams_P = teams_p;
            ViewBag.Project_P = project;      
            return View();
        }
    }
}
