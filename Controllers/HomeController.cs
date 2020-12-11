using Assesment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Assesment.Controllers
{
    [AllowAnonymous]

    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                bool isValid = db.Users.Any(a => a.userName == user.userName && a.userPassword == user.userPassword);
                if (isValid)
                {
                    FormsAuthentication.SetAuthCookie(user.userName, false);

                    //return RedirectToAction("Add", "Project");
                    return RedirectToAction("Index","Home");
                }
                ModelState.AddModelError("", "Invalid user / password");
                return View();
            }

        }
        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }
        [HttpGet]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
        [HttpPost]
        public ActionResult SignUp(User user)
        {
            MyLogger.GetInstance().Info("Creating User");
            try
            {
                using (DatabaseContext db = new DatabaseContext())
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                }
                MyLogger.GetInstance().Info("User Successfully Created");
            }
            catch(Exception ex)
            {

                MyLogger.GetInstance().Error("Error Creating User" + ex.Message);
            }

            return RedirectToAction("Login");

            
        }
        
        public ActionResult Index()
        {
            return View();
        }

      
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}