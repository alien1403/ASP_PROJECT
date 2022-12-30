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
    public class TasksController : Controller
    {

        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public TasksController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            db = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var tasks = db.Tasks.Include("Status").Include("User");
            ViewBag.Tasks = tasks;

            if (TempData.ContainsKey("message"))
            {
                ViewBag.Message = TempData["message"];
            }
            return View();
        }

        [Authorize(Roles = "User,Editor,Admin")]
        public IActionResult Show(int id)
        {
            Models.Task task = db.Tasks.Include("Status")
                                .Include("User")
                                .Include("Comments")
                                .Include("Comments.User")
                                .Where(task => task.Id == id)
                                .First();

            SetAccessRights();

            return View(task);
        }

        [HttpPost]
        [Authorize(Roles = "User,Editor,Admin")]
        public IActionResult Show([FromForm] Comment comment)
        {
            comment.Date = DateTime.Now;
            comment.UserId = _userManager.GetUserId(User);
            if (ModelState.IsValid)
            {
                db.Comments.Add(comment);
                db.SaveChanges();
                return Redirect("/Tasks/Show/" + comment.TaskId);
            }
            else
            {
                Models.Task task = db.Tasks.Include("Status")
                                .Include("User")
                                .Include("Comments")
                                .Include("Comments.User")
                                .Where(task => task.Id == comment.TaskId)
                                .First();
                SetAccessRights();
                return View(task);
            }
        }

        [Authorize(Roles = "User,Editor,Admin")]
        public IActionResult New(int id)
        {
            Models.Task task = new Models.Task();
            task.Stat = GetAllStats();
            task.IdProject = id;

            return View(task);
        }

        [HttpPost]
        //[Authorize(Roles = "User,Editor,Admin")]
        public IActionResult New(Models.Task task, int id)
        {
            task.StartDate = DateTime.Now;
            task.UserId = _userManager.GetUserId(User);

            if (ModelState.IsValid)
            {
                task.IdProject = id;
                db.Tasks.Add(task);
                Debug.WriteLine("<------------------------------------------------>");
                Debug.WriteLine($"{id}");
                Debug.WriteLine("<------------------------------------------------>");
                task.Id = new int();
                db.SaveChanges(); ;
                TempData["message"] = "Task-ul a fost adaugat";
                return RedirectToAction("View","Project", new {id = id});
            }
            else
            {
                //task.Stat = GetAllStats();
                return View(task);
            }
        }

        [Authorize(Roles = "User,Editor,Admin")]
        public IActionResult Edit(int id)
        {
            Models.Task task = db.Tasks.Include("Status")
                                       .Where(tsk => tsk.Id == id)
                                       .First();

            task.Stat = GetAllStats();

            if(task.UserId == _userManager.GetUserId(User) || User.IsInRole("Admin"))
            {
                return View(task);
            }
            else
            {
                TempData["message"] = "Nu aveti dreptul sa faceti modificari asupra unui task care nu va apartine";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [Authorize(Roles = "User,Editor,Admin")]
        public IActionResult Edit(int id, Models.Task requestTask)
        {
            Models.Task task = db.Tasks.Find(id);

            if (ModelState.IsValid)
            {
                if(task.UserId == _userManager.GetUserId(User) || User.IsInRole("Admin"))
                {
                    task.Title = requestTask.Title;
                    task.Description = requestTask.Description;
                    task.StatusId = requestTask.StatusId;
                    TempData["message"] = "Task-ul a fost modificat cu succes";
                    db.SaveChanges();
                    return RedirectToAction("View", "Project", new {id = task.IdProject});
                }
                else
                {
                    TempData["message"] = "Nu aveti dreptul sa faceti modificari asupra unui task care nu va apartine";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                requestTask.Stat = GetAllStats();
                return View(requestTask);
            }
        }

        [HttpPost]
        [Authorize(Roles = "User,Editor,Admin")]
        public IActionResult Delete(int id)
        {
            Models.Task task = db.Tasks.Include("Comments")
                                       .Where(tsk => tsk.Id == id)
                                       .First();

            if(task.UserId == _userManager.GetUserId(User) || User.IsInRole("Admin"))
            {
                db.Tasks.Remove(task);
                db.SaveChanges();
                TempData["message"] = "Task-ul a fost sters";
                return RedirectToAction("View", "Project", new { id = task.IdProject });
            }
            else
            {
                TempData["message"] = "Nu aveti dreptul sa stergeti un task care nu va apartine";
                return RedirectToAction("Index");
            }
        }

        [NonAction]
        public IEnumerable<SelectListItem> GetAllStats()
        {
            var selectList = new List<SelectListItem>();
            var statuses = from stat in db.Statuses
                           select stat;

            foreach (var stat in statuses)
            {
                selectList.Add(new SelectListItem
                {
                    Value = stat.Id.ToString(),
                    Text = stat.StatusName.ToString()
                });
            }

            return selectList;
        }

        private void SetAccessRights() {
            ViewBag.AfisareButoane = false;

            if (User.IsInRole("Admin"))
            {
                ViewBag.AfisareButoane = true;
            }
            ViewBag.EsteAdmin = User.IsInRole("Admin");

            ViewBag.UserCurent = _userManager.GetUserId(User);
        }
    }
}
