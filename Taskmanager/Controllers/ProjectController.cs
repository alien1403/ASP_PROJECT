using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;
using Taskmanager.Data;
using Taskmanager.Models;

namespace Taskmanager.Controllers
{
   
    public class ProjectController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> userManager;

        public ProjectController(ApplicationDbContext ctx, UserManager<ApplicationUser> uM)
        {
            db = ctx;
            userManager = uM;
        }

        [Authorize(Roles = "User,Editor,Admin")]

        public IActionResult Add(int id)
        {
            ViewBag.TeamID = id;
            Projects p = new Projects();
            return View(p);
        }
        [HttpPost]
        [Authorize(Roles = "User,Editor,Admin")]
        public IActionResult Add(Projects p)
        {
            if(p.IdTeam == null || p.Name == null || p.Description == null)
            {
                TempData["msg"] = "Aw snap! There was an input error";
                return RedirectToAction("Add", "Project", new { id = p.IdTeam });
            }

            p.IdAdmin = userManager.GetUserId(User);
            
           

            db.Projects.Add(p);
            db.SaveChanges();
            return RedirectToAction("View", "Team", new {id = p.IdTeam});
        }

        [Authorize(Roles = "User,Editor,Admin")]
        public IActionResult View(int id)
        {
            var h = from i in db.Projects
                    where i.Id == id
                    select i;

            var tasks = from t in db.Tasks
                        where t.IdProject == id
                        select t;
            ViewBag.Tasks = tasks;

         TempData["admin"] = String.Compare(h.First().IdAdmin, userManager.GetUserId(User)).ToString();

            return View(h.First());
        }
        [HttpPost]
        [Authorize(Roles = "User,Editor,Admin")]
        public IActionResult Delete(int id,int team_id)
        {
            var pr = from h in db.Projects
                     where h.Id == id
                     select h;
           
            if (pr.Count() > 0)
            {
                
                db.Projects.Remove(pr.First());
                
                db.SaveChanges();
            }
            Debug.WriteLine("<------------------------------------------------>");
            Debug.WriteLine($"{team_id}");
            Debug.WriteLine("<------------------------------------------------>");

            return RedirectToAction("View", "Team", new { id = team_id }); 
        }

        [Authorize(Roles = "User,Editor,Admin")]
        public IActionResult Edit(int id)
        {
            Projects p = new();

            p.Id = id;

            return View(p);
        }

        [HttpPost]
        [Authorize(Roles = "User,Editor,Admin")]
        public IActionResult Edit(Projects p)
        {
            var pr = db.Projects.Find(p.Id);
            if (!ModelState.IsValid)
            {
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {

                        Debug.WriteLine("<------------------------------------------------>");
                        Debug.WriteLine($"{error.ErrorMessage}");
                        Debug.WriteLine("<------------------------------------------------>");

                    }
                }
                return View(p);
            }

            if (pr != null)
            {   
                pr.Name = p.Name;
                pr.Description = p.Description;

                db.SaveChanges();
                return RedirectToAction("View", "Team", new { id = pr.IdTeam });
            }
            TempData["msg"] = "Ah snap! There was an error!";
            return RedirectToAction("Index", "Dashboard");



        }
    }
}
