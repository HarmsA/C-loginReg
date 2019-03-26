using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LoginReg.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace LoginReg.Controllers
{
    public class HomeController : Controller
    {
        private int? UserSession
        {
            get { return HttpContext.Session.GetInt32("UserId"); }
            set { HttpContext.Session.SetInt32("UserId", (int)value); }
        }
                private UserContext dbContext;
        public HomeController(UserContext context)
        {
            dbContext = context;
        }
        [Route("")]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [Route("create")]
        [HttpPost]
        public IActionResult CreateUser(User newUser)
        {
            if(ModelState.IsValid)
            {        
                if(dbContext.Users.Any(u => u.Email == newUser.Email))
                {
                    ModelState.AddModelError("Email", "Email already exists.");
                    return View("Index");
                }

                PasswordHasher<User> hasher = new PasswordHasher<User>();
                string hashedPW = hasher.HashPassword(newUser, newUser.Password);

                newUser.Password = hashedPW;

                dbContext.Users.Add(newUser);
                dbContext.SaveChanges();
                TempData["LoginMessage"] = "You may now log in!";
                return RedirectToAction("Login");
            }
            return View("Index");
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }
        
        [HttpGet("home")]
        public IActionResult LoggedInPage()
        {
            return View();
        }

        [HttpPost("verifyLogin")]
        public IActionResult VerifyLogin(LoginUser loginUser)
        {
            if(ModelState.IsValid)
            {
                User userlogin = dbContext.Users.FirstOrDefault(u => u.Email == loginUser.Email);
                if(userlogin == null)
                {
                    ModelState.AddModelError("Email", "Invalid Email/Password");
                    return View("Login");
                }
                else
                {
                    PasswordHasher<LoginUser> hasher = new PasswordHasher<LoginUser>();
                    var result = hasher.VerifyHashedPassword(loginUser, userlogin.Password, loginUser.Password);

                    if(result == 0)
                    {
                        ModelState.AddModelError("Email", "Invalid Email/Password");
                        return View("Login");
                    }
                    // Log them in with session
                    UserSession = userlogin.UserId;
                    return RedirectToAction("LoggedInPage");
                }
            }
            return View("Login");
        }

    }
}
