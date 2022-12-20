using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Taskmanager.Data;
using Taskmanager.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Web;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace Taskmanager.Controllers
{
    public class TeamController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> userManager;

        public TeamController(ApplicationDbContext ctx, UserManager<ApplicationUser> uM)
        {
            db = ctx;
            userManager = uM;

        }
        [Authorize(Roles = "User,Editor,Admin")]
        public IActionResult Create()
        {
            Team t = new();
            
            return View(t);
        }


        [HttpPost]
        [Authorize(Roles = "User,Editor,Admin")]
        public IActionResult Create(Team t)
        {
            t.IdAdmin = userManager.GetUserId(User);


            if (!ModelState.IsValid)
            {
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                      
                        Debug.WriteLine("<------------------------------------------------>");
                        Debug.WriteLine($"{error.ErrorMessage} <-->{t.IdAdmin}");
                        Debug.WriteLine("<------------------------------------------------>");

                    }
                }

                
                return View(t);
            }


            if (t.Name == null)
            {
                TempData["msg"] = " Input error, please try again!";

                return RedirectToAction("Create", "Team");
            }
            t.Name = HttpUtility.HtmlEncode(t.Name);

            t.IdAdmin = userManager.GetUserId(User);
             

                db.Teams.Add(t);
                db.SaveChanges();


            //Debug.WriteLine("<------------------------------------------------>");
            //Debug.WriteLine(t.Name, t.Id, t.IdAdmin);
            //Debug.WriteLine("<------------------------------------------------>");

            //foreach (var modelState in ModelState.Values)
            //{
            //    foreach (var error in modelState.Errors)
            //    {

            //        Debug.WriteLine("<------------------------------------------------>");
            //        Debug.WriteLine($"{error.ErrorMessage} <-->{t.Id}");
            //        Debug.WriteLine("<------------------------------------------------>");

            //    }
            //}



            return RedirectToAction("Index","Dashboard");
        }

        [Authorize(Roles = "User,Editor,Admin")]
        public IActionResult View(int id)
        {
            var t = from h in db.Teams where h.Id == id select h;
            var p = from h in db.Projects where h.IdTeam == id select h;

            var m = from h in db.Members
                    where h.IdTeam == id
                    join u in db.Users
                    on h.IdMember equals u.Id
                    select new { u.UserName, u.Email , u.Id};

            if (db.Teams.Find(id)==null)
            {
                TempData["msg"] = " Input error, there is no such team!";
 
                return RedirectToAction("Index", "Dashboard");
            }

            if(m.Where(h=>h.Id == userManager.GetUserId(User))==null && userManager.GetUserId(User) != db.Teams.Find(id).IdAdmin)
            {
                TempData["msg"] = " Aw snap! It looks like you are not in this Team!";

                return RedirectToAction("Index", "Dashboard");
            }

            if (t.Any())
            {
                var h = t.OfType<Team>();
              
                ViewBag.Projects = p;
                ViewBag.Members_P = m;

                var bb = String.Compare(db.Teams.Find(id).IdAdmin, userManager.GetUserId(User)).ToString();
                TempData["admin"] = String.Compare(db.Teams.Find(id).IdAdmin, userManager.GetUserId(User)).ToString();
                Debug.WriteLine($"{db.Teams.Find(id).IdAdmin} <------> {userManager.GetUserId(User)} <-------> {bb}");

                return View(t.First());
            }
        
            return RedirectToAction("Index", "Dashboard");
            
        }

        [Authorize(Roles = "User,Editor,Admin")]
        public IActionResult Edit(int id)
        {

            ViewBag.id = id;
            var t = db.Teams.Find(id);
            if(t == null)
                {
                TempData["msg"] = " Input error, please try again!";
              
                return RedirectToAction("View", "Team", new { id = id});
            }
            else if(userManager.GetUserId(User)!= t.IdAdmin)
            {
                TempData["msg"] = " You don't have the right to edit , please try again!";
                TempData["err_bool"] = "true";
                return RedirectToAction("View", "Team", new { id = id });
            }

            
            return View(t);
        }


        [HttpPost]
        [Authorize(Roles = "User,Editor,Admin")]
        public IActionResult Edit(Team t)
        {
         
            
            if (!ModelState.IsValid)
            {

                TempData["msg"] = " Input error, please try again!";
                



                return View(t);
            }
            var team = db.Teams.Find(t.Id);


            if (team  != null )
            {
                team.Name = HttpUtility.HtmlEncode(t.Name);
                db.SaveChanges();
            }

            return RedirectToAction("Index", "Dashboard");
        }

        [HttpPost]
        [Authorize(Roles = "User,Editor,Admin")]
        public IActionResult Delete(Team t)
        {

            if (t.Id != null && userManager.GetUserId(User) == t.IdAdmin)
            {
                db.Teams.Remove(t);
                db.SaveChanges();
            }
            else
            {
                if(t == null)
                    TempData["msg"] = " Input error, please try again!";
                else
                    TempData["msg"] = " You don't have the right to delete!";

                TempData["err_bool"] = "true";

                return RedirectToAction("View", "Team", new { id = t.Id });
            }
            return RedirectToAction("Index", "Dashboard");
        }
    }
}
