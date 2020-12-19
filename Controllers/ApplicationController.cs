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
            DbManagerFactory dbm = new DbManagerFactory(ProjectBaseClass.dbType());

            IProject ip = dbm.GetDbManager();

            List<Application> applist;
            
            applist = ip.GetApplications();
            
            return View(applist);
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

                DbManagerFactory dbm = new DbManagerFactory(ProjectBaseClass.dbType());

                IProject ip = dbm.GetDbManager();

                ActionResponse ar = new ActionResponse();

                ar=ip.UpdateApplication(app, saveasnew);

                if (!ar.IsSuccess)
                {
                    //return RedirectToAction("ErrorView", "Project", new { message = ar.ErrorMsg });
                }

                applist = ar.obj;
            }

            return PartialView("~/Views/Application/ShowApps.cshtml", applist);
            //  return Json(app, JsonRequestBehavior.AllowGet);
        }
    }
}