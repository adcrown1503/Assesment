using Assesment.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assesment.Controllers
{
    public class ApplicationController : Controller
    {
        // GET: Application
        
        public ActionResult Index()
        {
            try {
                DatabaseContext db = new DatabaseContext();
                List<Application> applist = db.Applications.OrderByDescending(f => f.appid).ToList();
                return View(applist);
            }
            catch(Exception ex)
            {
                return RedirectToAction("IndexError", "Project", new { msg = ex.Message });

            }
          
        }

        [HttpGet]
        public ActionResult Test()
        {
            // DatabaseContext db = new DatabaseContext();
            List<SelectListItem> ImpactType = new List<SelectListItem>()
            {

                new SelectListItem() { Value = "None", Text = "None" },
                new SelectListItem() { Value = "Minor", Text = "Minor" },
                new SelectListItem() { Value = "Medium", Text = "Medium" },
                new SelectListItem() { Value = "Large", Text = "Large" },
                new SelectListItem() { Value = "Very Large", Text = "Very Large" },
            };

            //SelectListItem selListItem0 = new SelectListItem() { Value = "None", Text = "None" };
            //SelectListItem selListItem1 = new SelectListItem() { Value = "Minor", Text = "Minor" };
            //SelectListItem selListItem2 = new SelectListItem() { Value = "Medium", Text = "Medium" };
            //SelectListItem selListItem3 = new SelectListItem() { Value = "Large", Text = "Large" };
            //SelectListItem selListItem4 = new SelectListItem() { Value = "Very Large", Text = "Very Large" };

            //ImpactType.Add(selListItem0);
            //ImpactType.Add(selListItem1);
            //ImpactType.Add(selListItem2);
            //ImpactType.Add(selListItem3);
            //ImpactType.Add(selListItem4);

            ViewBag.Impact = ImpactType;
            ViewBag.impactType = "Large";
            return View();


        }

        [HttpPost]
        public ActionResult Add(Application app,string saveasnew)
        {

            MyLogger.GetInstance().Info("Updating Application");
            List<Application> applist=null;
            if (ModelState.IsValid)
            {
                DatabaseContext db = new DatabaseContext();
                db.Applications.Add(app);

                if (app.appid > 0 && saveasnew == "OFF")
                {
                    db.Entry(app).State = EntityState.Modified;

                }

                if (app.appid > 0 && saveasnew == "Delete")
                {
                    db.Entry(app).State = EntityState.Deleted;
                }
                    

                db.SaveChanges();
                // return RedirectToAction("Index");

                applist = db.Applications.OrderByDescending(f=>f.appid).ToList();
                MyLogger.GetInstance().Info("Application Updated Successfully");
            }
            
            return PartialView("~/Views/Application/ShowApps.cshtml", applist);
            //  return Json(app, JsonRequestBehavior.AllowGet);
        }
    }
}