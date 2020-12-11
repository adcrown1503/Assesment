using Assesment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assesment.Controllers
{
    public class TestController : Controller
    {
        // GET: Testss
        [HttpGet]
        public ActionResult Index()
        {
            Project prj = new Project();
            prj.impactType = "Medium";
            prj.statusId = 1;
            return View(prj);
        }
    }
}