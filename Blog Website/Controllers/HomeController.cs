using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog_Website.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blog_Website.Controllers {
    public class HomeController : Controller {
        public IActionResult Index() {
            return View();
        }
        [HttpGet]
         public IActionResult SignIn() {
            return View("SignIn");
        }
        [HttpPost]
        public IActionResult SignIn(SignInHelper user, string password , string email) {
            User signedInUser = UserRepository.SignInUser(email, password);
            if (signedInUser == null) {
                SignInHelper signInHelper = new SignInHelper();
                signInHelper.message = "The username or password you entered is wrong";
                return View("SignIn", signInHelper);
            }
            HttpContext.Response.Cookies.Append("loggedInUserId", signedInUser.userID.ToString());
            HttpContext.Response.Cookies.Append("user_name", $"{signedInUser.FName}");

            return RedirectToAction("Profile", "User");
            
        }
        [HttpPost]
        public IActionResult SignUp(User user) {

            bool error = UserRepository.registerUser(user);
            string Msg;
            SignInHelper err = new SignInHelper();
            if (error) {
                err.message = "Successfully Signed Up. Please Sign In"; 
                return View("SignIn", err);
            }
            else {
                User temp = new User();
                temp.ErrMessage = "The email account already exists. Please try another";
                return View("Index", temp);
            }
        }
    }
}
