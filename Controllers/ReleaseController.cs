using Assesment.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assesment.Controllers
{
    
    public class ReleaseController : Controller
    {
        // GET: Release
        public ActionResult Index()
        {
            try
            {
                DatabaseContext db = new DatabaseContext();
                Release rel = new Release();
                List<Release> rellist = db.Releases.OrderByDescending(r => r.releaseId).ToList();
                return View(rellist);
            }
            catch(Exception ex)
            {
                return RedirectToAction("IndexError", "Project", new { msg = ex.Message });

            }
         

          
        }
        [HttpPost]
        public ActionResult Create(Release release, string RecType)
        {
            MyLogger.GetInstance().Info("Updating Release");

            DatabaseContext db = new DatabaseContext();
            
            if (ModelState.IsValid)
            {
                db.Releases.Add(release);

                if ( RecType == "Edit")
                {
                    db.Entry(release).State = EntityState.Modified;

                }
                if ( RecType == "Delete")
                {
                    db.Entry(release).State = EntityState.Deleted;
                }

                db.SaveChanges();

                MyLogger.GetInstance().Info("Release Updated Successfully");
            }

            List<Release> rellist = db.Releases.OrderByDescending(r => r.releaseId).ToList();
          //  return Json("true", JsonRequestBehavior.AllowGet);
            return PartialView("~/Views/Release/ShowRelease.cshtml", rellist);
           
        }
    }
}