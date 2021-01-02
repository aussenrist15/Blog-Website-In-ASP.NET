using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog_Website.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blog_Website.Controllers {
    public class BlogController : Controller {
        public IActionResult Index() {
            List<Blog> blogs = BlogRepository.GetAllBlogs();
            return View("Index", blogs);
        }
        public IActionResult Edit(int id) {
            if (!HttpContext.Request.Cookies.ContainsKey("loggedInUserId")) {
                return RedirectToAction("SignIn", "Home");
            }
            Blog blog = BlogRepository.getBlogByID(id.ToString());
            if(blog == null) {
                return RedirectToAction("Profile", "User");
            }
            string ID = HttpContext.Request.Cookies["loggedInUserId"];
            if(blog.userID.ToString() != ID) {
                return RedirectToAction("Profile", "User");
            }
            return View("Edit", blog);
        }
        [HttpPost]
        public IActionResult Edit(Blog blog) {
            if (!HttpContext.Request.Cookies.ContainsKey("loggedInUserId")) {
                return RedirectToAction("SignIn", "Home");
            }
            BlogRepository.EditBlog(blog);
            return RedirectToAction("Profile", "User");
        }
        public IActionResult Create() {
            if (!HttpContext.Request.Cookies.ContainsKey("loggedInUserId")) {
                return RedirectToAction("SignIn", "Home");
            }
            return View("Create");
        }
        [HttpPost]
        public IActionResult Create(Blog_Website.Models.Blog blog) {
            if (!HttpContext.Request.Cookies.ContainsKey("loggedInUserId")) {
                return RedirectToAction("SignIn", "Home");
            }
            string ID = HttpContext.Request.Cookies["loggedInUserId"];
            blog.blogDate = DateTime.Now;
            blog.userID = int.Parse(ID);
            User user = UserRepository.getUserById(ID);
            blog.userName = user.FName;
            BlogRepository.AddBlog(blog);
            return RedirectToAction("Profile", "User");
        }
        public IActionResult Delete(int id) {
            if (!HttpContext.Request.Cookies.ContainsKey("loggedInUserId")) {
                return RedirectToAction("SignIn", "Home");
            }
            string ID = HttpContext.Request.Cookies["loggedInUserId"];
            Blog blog = BlogRepository.getBlogByID(id.ToString());
            if (blog.userID != int.Parse(ID)) {
                return RedirectToAction("Profile", "User");
            }
            BlogRepository.DeleteBlog(id);
            return RedirectToAction("Profile", "User");
            
        }
    }
}
