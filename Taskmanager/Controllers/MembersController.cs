using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using Taskmanager.Data;
using Taskmanager.Models;

//check Delete for non-admin user option

namespace Taskmanager.Controllers
{
    public class MembersController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> userManager;

        public MembersController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }
        [Authorize(Roles = "User,Editor,Admin")]
        public IActionResult Add(int id)
        {
            ViewBag.MemberController_IdTeam = id;
            if (userManager.GetUserId(User) != db.Teams.Find(id).IdAdmin)
            {
                TempData["msg"] = "Aw snap! You don't have the right to add memebers!";
                return RedirectToAction("View","Team", new {id = id});
            }
            ApplicationUser u = new();
            
            
            return View(u);
        }
        [HttpPost]
        [Authorize(Roles = "User,Editor,Admin")]
        public IActionResult Add(ApplicationUser v, int IdTeam)
        {
            Debug.Write($"<--------{ModelState.IsValid}------->");

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


                return View(v);
            }

            int id_team = IdTeam;
            var email = v.Email;
            var usr = from u in db.Users
                      where u.Email == email
                      select u;
            if (usr.Count() > 0)
            {
               
                var member = new Member();
                member.IdTeam = id_team;
                member.IdMember = usr.First().Id;

                if (db.Members.Find( member.IdMember, member.IdTeam )==null )
                {
                    db.Members.Add(member);
                    db.SaveChanges();
                }
            }
            Debug.WriteLine("<------------------------------------------------>");
            Debug.WriteLine($"{id_team} {email}");
            Debug.WriteLine("<------------------------------------------------>");
            return RedirectToAction("View", "Team",new {id = id_team});

        }
        [HttpPost]
        [Authorize(Roles = "User,Editor,Admin")]
        public IActionResult Delete(string id, int team_id)
        {
            if (String.Compare(id, "-1") == 0)
            {
                db.Members.Remove(db.Members.Find(userManager.GetUserId(User), team_id));
                db.SaveChanges();
                return RedirectToAction("Index", "Dashboard", new { id = team_id });
            }

            var member = from m in db.Members
                         where m.IdMember == id
                         where m.IdTeam == team_id
                         select m;


            if (db.Teams.Find(team_id).IdAdmin != userManager.GetUserId(User))
            {
                TempData["msg"] = " You don't have the right to delete!";
                return RedirectToAction("View", "Team", new { id = team_id });
            }
            else if (member.Count() > 0 )
            {
                db.Members.Remove(member.First());
                db.SaveChanges();
            }
         
            return RedirectToAction("View", "Team", new {id=team_id});
        }
        [HttpPost]
        [Authorize(Roles = "User, Editor, Admin")]
        public IActionResult Edit(string id, int team_id)
        {
           

            db.Teams.Find(team_id).IdAdmin = id;
            

            Member m = new Member();

            m.IdMember = userManager.GetUserId(User);
            m.IdTeam = team_id;

            

           
            db.Members.Remove(db.Members.Find(id,team_id));
            db.Members.Add(m);
            db.SaveChanges();

            return RedirectToAction("View", "Team", new { id = team_id });
        }
        
       
        
    }
}
