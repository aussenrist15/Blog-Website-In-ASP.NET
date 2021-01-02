using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog_Website.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blog_Website.Controllers {
    public class UserController : Controller {
        public IActionResult Index() {
            return View();

        }
         public IActionResult Blogs() {
            
            return View("Blogs");
        }
        
        public IActionResult Profile() {
            if (!HttpContext.Request.Cookies.ContainsKey("loggedInUserId")) {
                return RedirectToAction("SignIn", "Home");
            }
            string ID = HttpContext.Request.Cookies["loggedInUserId"];
            User current = UserRepository.getUserById(ID);
            current.userBlogs = BlogRepository.GetBlogsForUser(ID);
            return View("Profile", current);
        }

        public IActionResult Logout() {
            HttpContext.Response.Cookies.Delete("loggedInUserId");
            HttpContext.Response.Cookies.Delete("user_name");
            return RedirectToAction("SignIn", "Home");
        }

        public IActionResult Edit(int id) {
            if (!HttpContext.Request.Cookies.ContainsKey("loggedInUserId")) {
                return RedirectToAction("SignIn", "Home");
            }
            string ID = HttpContext.Request.Cookies["loggedInUserId"];
            if(id.ToString() != ID) {
                User curr = UserRepository.getUserById(ID);
                curr.userBlogs = BlogRepository.GetBlogsForUser(ID);
                return View("Profile", curr);
            }
            User user = UserRepository.getUserById(id.ToString());
            return View("EditProfile", user);
        }
        [HttpPost]
        public IActionResult Edit(User user) {
           User updated = UserRepository.UpdateUser(user);
            HttpContext.Response.Cookies.Append("user_name", updated.FName);
            updated.userBlogs = BlogRepository.GetBlogsForUser(updated.userID.ToString());
            return View("Profile", updated);
        }
    }
}
