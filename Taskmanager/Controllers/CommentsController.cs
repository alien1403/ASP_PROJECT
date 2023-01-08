﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Security.Application;
using Taskmanager.Data;
using Taskmanager.Models;

namespace Taskmanager.Controllers
{
    public class CommentsController : Controller
    {

        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public CommentsController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager
        )
        {
            db = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        [HttpPost]
        [Authorize(Roles = "User,Editor,Admin")]
        public IActionResult Delete(int id)
        {
            Comment comm = db.Comments.Find(id);
            if (comm.UserId == _userManager.GetUserId(User) || User.IsInRole("Admin")) {
                db.Comments.Remove(comm);
                db.SaveChanges();
                return RedirectToAction("Show","Tasks", new { id = comm.TaskId });
            }
            else
            {
                TempData["message"] = "Nu aveti dreptul sa stergeti comentariul";
                return RedirectToAction("Index", "Tasks");
            }
        }

        [Authorize(Roles = "User,Editor,Admin")]
        public IActionResult Edit(int id)
        {
            Comment comm = db.Comments.Find(id);
     
            if(comm.UserId == _userManager.GetUserId(User) || User.IsInRole("Admin"))
            {
                return View(comm);
            }
            else
            {
                TempData["message"] = "Nu aveti dreptul sa editati comentariul";
                return RedirectToAction("Index", "Tasks");
            }
        }

        [HttpPost]
        [Authorize(Roles = "User,Editor,Admin")]
        public IActionResult Edit(int id, Comment requestComment)
        {
            Comment comm = db.Comments.Find(id);
            if(comm.UserId == _userManager.GetUserId(User) || User.IsInRole("Admin"))
            {
                if (ModelState.IsValid)
                {
                    comm.Content = AntiXss.HtmlEncode(requestComment.Content);
                    db.SaveChanges();
                    return Redirect("/Tasks/Show/" + comm.TaskId);
                }
                else
                {
                    return View(requestComment);
                }
            }
            else
            {
                TempData["message"] = "Nu aveti dreptul sa faceti modificari";
                return RedirectToAction("Index", "Tasks");
            }
        }
    }
}
