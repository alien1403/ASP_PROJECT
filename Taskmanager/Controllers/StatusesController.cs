using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Taskmanager.Data;
using Taskmanager.Models;

namespace Taskmanager.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StatusesController : Controller
    {

        private readonly ApplicationDbContext db;

        public StatusesController(ApplicationDbContext context) {
            db = context;
        }
        public ActionResult Index()
        {
            var user = HttpContext.User.Identity.Name;


            Console.WriteLine(user);
            if (TempData.ContainsKey("message"))
            {
                ViewBag.Message = TempData["message"].ToString();
            }
            var statuses = from status in db.Statuses
                           orderby status.StatusName
                           select status;
            ViewBag.Statuses = statuses;
            return View();
        }
        [Authorize(Roles = "User,Editor,Admin")]
        public ActionResult Show(int id)
        {
            Status status = db.Statuses.Find(id);
            return View(status);
        }
        [Authorize(Roles = "User,Editor,Admin")]
        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "User,Editor,Admin")]
        public IActionResult New(Status status)
        {
            Console.WriteLine(status.StatusName);
            Console.WriteLine(ModelState.IsValid);
            if (ModelState.IsValid)
            {
                db.Statuses.Add(status);
                db.SaveChanges();
                TempData["message"] = "Statusul a fost adaugat";
                return RedirectToAction("Index");
            }
            else
            {
                return View(status);
            }
        }
        [Authorize(Roles = "User,Editor,Admin")]
        public IActionResult Edit(int id)
        {
            Status status = db.Statuses.Find(id);
            return View(status);
        }

        [HttpPost]
        [Authorize(Roles = "User,Editor,Admin")]
        public IActionResult Edit(int id, Status status)
        {
            Status newStatus = db.Statuses.Find(id);
            Console.WriteLine(newStatus.StatusName);
            if (!ModelState.IsValid)
            {
                newStatus.StatusName = status.StatusName; ;
                db.SaveChanges();
                TempData["message"] = "Statusul a fost modificat";
                return RedirectToAction("Index");
            }
            else
            {
                return View(status);
            }
        }

        [HttpPost]
        [Authorize(Roles = "User,Editor,Admin")]
        public ActionResult Delete(int id)
        {
            Status status = db.Statuses.Find(id);
            db.Statuses.Remove(status);
            TempData["message"] = "Statusul a fost sters";
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
