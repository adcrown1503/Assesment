using Assesment.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assesment.Controllers
{
    
    public class ProjectController : Controller
    {
        public string msg;
        public string dbType= "Oracle";
       

        // private static Logger logger = LogManager.GetLogger("myAppLoggerRules");

        [HttpPost]
        public ActionResult UploadFile(FormCollection forms, HttpPostedFileBase file) //
        {
            string fileName = "";
            int projectid = Convert.ToInt32(forms["projectId"]);
            int gateid = Convert.ToInt32(forms["gateId"]);
            int line = Convert.ToInt32(forms["lineNo"]);
            int attachId = Convert.ToInt32(forms["attachId"]);

            if (attachId > 0 && file is null)
            {
                //System.Diagnostics.Debug.WriteLine("delete attachement");
                DeleteAttachment(attachId);
            }
            else
            {
                try
                {

                    fileName = Path.GetFileName(file.FileName);
                    string filepath = Path.Combine(Server.MapPath("~/UploadedFiles"), fileName);
                    file.SaveAs(filepath);
                }
                catch
                {
                    return RedirectToAction("GateDetail", "Project", new { @projectId = projectid, @gateId = gateid, @gateLine = line });

                }

                using (DatabaseContext db = new DatabaseContext())
                {
                    EstimateAttachment ea = new EstimateAttachment();
                    ea.projectId = projectid;
                    ea.gateId = gateid;
                    ea.lineNo = line;
                    ea.fileName = fileName;

                    db.EstimateAttachments.Add(ea);
                    db.SaveChanges();
                }
                // System.Diagnostics.Debug.WriteLine("upload file");
            }


            return RedirectToAction("GateDetail", "Project", new { @projectId = projectid, @gateId = gateid, @gateLine = line });
        }
        [NonAction]
        public string DeleteAttachment(int id)
        {

            int projectid = 0;
            int gateid = 0;
            int line = 0;
            string result = "";
            using (DatabaseContext db = new DatabaseContext())
            {
                try
                {

                    EstimateAttachment ea = db.EstimateAttachments.Where(x => x.attacheId == id).First();
                    // db.EstimateAttachments.Add(ea);

                    projectid = ea.projectId;
                    gateid = ea.gateId;
                    line = ea.lineNo;

                    db.Entry(ea).State = EntityState.Deleted;


                    db.SaveChanges();

                    string fullPath = Request.MapPath("~/UploadedFiles/" + ea.fileName);

                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }

                    result = "Success";
                }
                catch (Exception ex)
                {

                    result = "No";
                    MyLogger.GetInstance().Error(ex.Message);
                }



            }

            return result;
        }
        //[HttpPost]
        //public ActionResult DeleteAttachmentxxx(int id)
        //{
        //    int projectid = 0;
        //    int gateid = 0;
        //    int line = 0;

        //    using (DatabaseContext db = new DatabaseContext())
        //    {

        //        EstimateAttachment ea = db.EstimateAttachments.Where(x => x.attacheId == id).First();
        //        // db.EstimateAttachments.Add(ea);

        //        projectid = ea.projectId;
        //        gateid = ea.gateId;
        //        line = ea.lineNo;

        //        db.Entry(ea).State = EntityState.Deleted;


        //        db.SaveChanges();

        //        string fullPath = Request.MapPath("~/UploadedFiles/" + ea.fileName);
        //        if (System.IO.File.Exists(fullPath))
        //        {
        //            System.IO.File.Delete(fullPath);
        //        }
        //    }
        //    return RedirectToAction("GateDetail", "Project", new { @projectId = projectid, @gateId = gateid, @gateLine = line });
        //}
        [HttpPost]
        public ActionResult CopyGate(FormCollection forms)
        {
            
            int projectid = Convert.ToInt32(forms[forms.AllKeys[0]]);
            int gateid = Convert.ToInt32(forms[forms.AllKeys[1]]);
            int line = Convert.ToInt32(forms[forms.AllKeys[2]]);
            int newgate = Convert.ToInt32(forms[forms.AllKeys[3]]);

            SQLManager pm = new SQLManager();
            ActionResponse ar=pm.GateSaveAs(projectid, gateid, line, newgate);


            return RedirectToAction("Editv02", "Project", new { @id = projectid });

        }
        [HttpPost]
        public ActionResult Edit(WrapperUpdateProject model)
        {
            ActionResponse ar = new ActionResponse();
            string output = "";
            MyLogger.GetInstance().Info("Updating Gate Info .." + model.gateVersion.gvProjectId);

            if (ModelState.IsValid)
            {
                SQLManager pm = new SQLManager();
                ar = pm.UpdateGateInfo(model);

                if (!ar.IsSuccess)
                {
                    return RedirectToAction("ErrorView", "Project", new { msg = ar.ErrorMsg});
                }
                else
                {
                    output = "gate info updated successfully";

                    MyLogger.GetInstance().Info("Updated gate info successfully.." + model.gateVersion.gvProjectId);
                }
            }
            

            return Json(output, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult Add(FormCollection form)  //, GateVersion gateversion, Project project
        {

            MyLogger.GetInstance().Info("Creating New Project");

            string objString = form["txtObject"];

            msg = "";

            WrapperUpdateProject model = JsonConvert.DeserializeObject<WrapperUpdateProject>(objString);


            if (ModelState.IsValid)
            {
                SQLManager pm = new SQLManager();

                bool result= pm.AddNewProject(model);

                if (result == false)
                {
                    msg="Error - saving project";

                    MyLogger.GetInstance().Error(msg);

                    return RedirectToAction("ErrorView", "Project", new { message = msg});
                    
                }
               
            }

            return RedirectToAction("Show", "Project");
        }

        [HttpPost]
        public ActionResult Create(WrapperViewNewProject wp)
        {
            //DatabaseContext db = new DatabaseContext();
            //List<Gate> gateList = db.Gates.ToList();
            //wp.gateList = gateList;
            //// wp.gate.gateId = 1;
            // System.Diagnostics.Debug.WriteLine("Hello");
            return RedirectToAction("index", "home");
        }

        [HttpGet]
        public JsonResult CheckName(Project project)
        {

            using (DatabaseContext db = new DatabaseContext())
            {

                return Json(!db.Projects.Any(x => x.name == project.name), JsonRequestBehavior.AllowGet);


            }
        }

        [HttpGet]
        public ActionResult Create2()
        {
            Project p = new Project();
            return View(p);
        }
        //[HttpGet]
        //public ActionResult Create()
        //{

        //    WrapperProject wp = new WrapperProject();
        //    using (DatabaseContext db = new DatabaseContext())
        //    {


        //        Project p = new Project();
        //        GateVersion gv = new GateVersion();
        //        ProjectStatus ps = new ProjectStatus();
        //        Gate g = new Gate();

        //        List<ProjectStatus> statusList = db.ProjectStatuses.ToList();
        //        List<Gate> gateList = db.Gates.ToList();
        //        List<Functionality> functionList = db.Functionalities.ToList();
        //        List<Phase> phaseList = db.Phases.ToList();
        //        List<Role> roleList = db.Roles.ToList();
        //        List<Application> appList = db.Applications.ToList();
        //        List<Release> releaseList = db.Releases.ToList();

        //        wp.functionList = functionList;
        //        wp.projectStatus = ps;
        //        wp.project = p;
        //        wp.project.statusId = 1;
        //        p.assesmentDate = DateTime.Today;
        //        p.desiredReleaseDate = DateTime.Today;
        //        wp.gateversion = gv;
        //        wp.statusList = statusList;
        //        wp.gateList = gateList;
        //        wp.functionList = functionList;
        //        wp.phaselist = phaseList;
        //        wp.roleList = roleList;
        //        wp.appList = appList;
        //        wp.relList = releaseList;
        //        wp.gate = g;
        //    }

        //    return View(wp);
        //}
     
       
        [HttpGet]
        public ActionResult Add()
        {

             MyLogger.GetInstance().Info("Creating Project");

            DbManagerFactory dbm = new DbManagerFactory(ProjectBaseClass.dbType());

            IProject Dbm = dbm.GetDbManager();

            WrapperViewNewProject nvp = Dbm.GetViewForNewProject();

                       
            if (nvp == null)
            {
                msg = "Error - getting views for new project";

                MyLogger.GetInstance().Error(msg);

                return RedirectToAction("ErrorView", "Project", new { message=msg });
            }

            return View(nvp);
        }

        [HttpGet]
        public ActionResult Show()
        {
            MyLogger.GetInstance().Info("Editing Project");

            SQLManager pm = new SQLManager();
           
            return View(pm.ShowProjects());
        }

        [HttpPost]
        public ActionResult SaveProjectInfo(WrapperViewEditProject we)
        {

            bool result = false;
            msg = "";

            MyLogger.GetInstance().Info("Udating Project Info");

            if (ModelState.IsValid)
            {
                SQLManager pm = new SQLManager();
                result=pm.SaveProjectInfo(we);

                if (result == false)
                {
                    msg = "Error updating project" + we.project.name;

                    MyLogger.GetInstance().Error(msg);
                    return RedirectToAction("ErrorView", "Project", new { message = msg });
                }
                else
                {
                    msg = "successfully updated project info " + we.project.name;
                    MyLogger.GetInstance().Info(msg);
                }
                
            }
                // var output = "yahoo";
                return Json(msg, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Editv02(int id)
        {
            //string msg = "";
            MyLogger.GetInstance().Info("Editing Project");

            SQLManager pm = new SQLManager();

            WrapperViewEditProject wp = pm.GetViewForUpdateProject(id);

            if (wp.project == null)
            {
                msg = "Error getting view for editing";

                MyLogger.GetInstance().Error(msg);

                return RedirectToAction("ErrorView", "Project", new { message = msg });
            }
            
            return View(wp);
        }

        [HttpGet]
        public ActionResult GateDetail(int projectId, int gateId, int gateLine)
        {

          //  string msg = "";

            MyLogger.GetInstance().Info("Getting Gate Details");

            SQLManager pm = new SQLManager();


            WrapperViewEditProject wp = pm.GetViewForUpdateGate(projectId,gateId,gateLine);

            if (wp == null)
            {
                msg = "Error getting gate information";

                MyLogger.GetInstance().Error(msg);

                return RedirectToAction("ErrorView", "Project", new { message = msg });
            }
            else
            {
                MyLogger.GetInstance().Info("gate info sucessfully retrieved");

            }


            return View(wp);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            WrapperViewEditProject wp = new WrapperViewEditProject();
            using (DatabaseContext db = new DatabaseContext())
            {
                //  ArrayList dayslist = new ArrayList();
                List<GroupFunction> dayslist = new List<GroupFunction>();
                try
                {
                    List<DbViewClass> gridClassList = db.gridClasses.AsNoTracking().Where(n => n.id == id).OrderBy(n => n.ProjFuncId).
                        ToList();

                    int tempFuncId = 0;
                    string str = "";
                    string flag = "X";
                    int printflag = 0;
                    int idfunc = 0;
                    int idrel = 0;
                    int idapp = 0;
                    int estimateRecId = 0;
                    string namefunc = "";
                    int totalDays = 0;
                    int projfuncid = 0;
                    GroupFunction gf = null;
                    foreach (var aa in gridClassList)
                    {


                        if (aa.FuncId != tempFuncId)
                        {
                            if (printflag == 1)
                            {
                                gf = new GroupFunction();
                                gf.appid = idapp;
                                gf.releaseId = idrel;
                                gf.FuncId = idfunc;
                                gf.FuncName = namefunc;
                                gf.daysString = str;
                                gf.totalDays = totalDays;
                                gf.ProjFuncId = projfuncid;
                                dayslist.Add(gf);
                                totalDays = 0;
                                str = "";
                            }
                            printflag = 1;

                        }
                        if (str.Length == 0)
                        {
                            str = aa.estId.ToString() + "," + aa.phaseCode.ToString() + "," + aa.roleId.ToString() + "," + aa.estDays.ToString() + "," + flag;
                        }
                        else
                        {
                            str = str + "-" + aa.estId.ToString() + "," + aa.phaseCode.ToString() + "," + aa.roleId.ToString() + "," + aa.estDays.ToString() + "," + flag;
                        }

                        idfunc = aa.FuncId;
                        idrel = aa.RelId;
                        idapp = aa.AppId;
                        namefunc = aa.FuncName;
                        estimateRecId = aa.estId;
                        projfuncid = aa.ProjFuncId;

                        totalDays = totalDays + aa.estDays;
                        tempFuncId = aa.FuncId;


                    }
                    gf = new GroupFunction();
                    gf.appid = idapp;
                    gf.releaseId = idrel;
                    gf.FuncId = idfunc;
                    gf.FuncName = namefunc;
                    gf.daysString = str;
                    gf.totalDays = totalDays;
                    gf.ProjFuncId = projfuncid;
                    dayslist.Add(gf);
                    //foreach (GroupFunction i in dayslist)
                    //{
                    //    System.Diagnostics.Debug.WriteLine(i.FuncId);
                    //    System.Diagnostics.Debug.WriteLine(i.FuncName);
                    //    System.Diagnostics.Debug.WriteLine(i.appid);
                    //    System.Diagnostics.Debug.WriteLine(i.RelId);
                    //    System.Diagnostics.Debug.WriteLine(i.daysString);

                    //}

                    //List<GroupFunction> result = gridClassList.GroupBy(x => new { x.FuncId, x.FuncName, x.AppId, x.RelId })
                    //    .Select(o => o.First()).ToList().Select(o => new GroupFunction
                    //    {
                    //        FuncId = o.FuncId, FuncName = o.FuncName, appid = o.AppId, RelId = o.RelId
                    //    }).ToList();



                    //foreach (GroupFunction item in result)
                    //{
                    //    System.Diagnostics.Debug.WriteLine(item.appid);
                    //}



                    List<Phase> phaseList = db.Phases.ToList();
                    List<Role> roleList = db.Roles.ToList();
                    List<Application> appList = db.Applications.ToList();
                    List<Release> releaseList = db.Releases.ToList();
                    List<Functionality> functionList = db.Functionalities.ToList();
                    List<ProjectStatus> statusList = db.ProjectStatuses.ToList();
                    List<Gate> gateList = db.Gates.ToList();


                    //   

                    wp.groupFunction = dayslist;
                    wp.project.impactType = gridClassList[0].impactType;
                    wp.project.statusId = gridClassList[0].statusId;
                    wp.gateversion.gvGateId = gridClassList[0].gvGateId;
                    wp.gateversion.gvStatus = gridClassList[0].gvStatus;
                    // wp.FuncId = gridClassList[0].FuncId;

                    wp.dbview = gridClassList;
                    wp.appList = appList;
                    wp.relList = releaseList;
                    wp.roleList = roleList;
                    wp.phaselist = phaseList;
                    wp.functionList = functionList;
                    wp.statusList = statusList;
                    wp.gateList = gateList;



                }
                catch (Exception ex)
                {
                   // string msg = "";
                    if (ex.InnerException is null)
                    {
                        msg = ex.Message;

                    }
                    else
                    {
                        msg = ex.InnerException.Message;

                    }
                    return RedirectToAction("IndexError", "Project", new { msg = msg });

                }
            }

            return View(wp);
        }
        //[HttpGet]
        //public ActionResult Editxxx(int id)
        //{

        //    WrapperProject wp = new WrapperProject();

        //    using (DatabaseContext db = new DatabaseContext())
        //    {
        //        try

        //        {

        //            Project project = db.Projects.SingleOrDefault(m => m.id == id);
        //            GateVersion gateversion = db.GateVersions.SingleOrDefault(m => m.gvProjectId == id);
        //            List<ProjectFunction> projectFunctionList = db.ProjectFunctions.Where(m => m.GateversionId == gateversion.gvId).ToList();
        //            List<ProjectStatus> statusList = db.ProjectStatuses.ToList();
        //            List<Gate> gateList = db.Gates.ToList();
        //            List<Functionality> functionList = db.Functionalities.ToList();
        //            List<Phase> phaseList = db.Phases.ToList();
        //            List<Role> roleList = db.Roles.ToList();
        //            List<Application> appList = db.Applications.ToList();
        //            List<Release> releaseList = db.Releases.ToList();
        //            List<Estimate> estimateList = db.Estimates.ToList();

        //            var aaa = projectFunctionList.Join(functionList, p => p.FuncId, f => (Int32?)(f.FuncId), (p, f) => new
        //            {
        //                p = p,
        //                f = f
        //            }).Join(estimateList, temp0 => (Int32?)(temp0.p.ProjFuncId), e => e.projectFunctionId,
        //                         (temp0, e) => new { temp0 = temp0, e = e })
        //            .Where(temp1 => (temp1.temp0.p.GateversionId == (Int32?)22))
        //                           .Select(temp1 => new
        //                           {
        //                               projectFuncId = temp1.temp0.p.ProjFuncId,
        //                               FUNC_ID = temp1.temp0.p.FuncId,
        //                               FUNC_NAME = temp1.temp0.f.FuncName,
        //                               APP_ID = temp1.temp0.p.AppId,
        //                               REL_ID = temp1.temp0.p.RelId,
        //                               EST_DAYS = temp1.e.estDays,
        //                               EST_PHASECODE = temp1.e.phaseCode,
        //                               EST_ROLEID = temp1.e.roleId
        //                           });

        //            wp.project = project;
        //            wp.gateversion = gateversion;
        //            wp.functionList = functionList;
        //            wp.statusList = statusList;
        //            wp.gateList = gateList;
        //            wp.phaselist = phaseList;
        //            wp.roleList = roleList;
        //            wp.appList = appList;
        //            wp.relList = releaseList;

        //        }
        //        catch (Exception ex)
        //        {
        //            return RedirectToAction("IndexError", "Project", new { msg = ex.Message });

        //        }
        //    }
        //    return View(wp);

        //}

        [HttpPost]

        public ActionResult RemoveEstimate(int pid, int gid, int gl)
        {
            SQLManager pm = new SQLManager();
            ActionResponse ar = new ActionResponse();
            ar = pm.GateRemove(pid, gid, gl);
            if (ar.IsSuccess)
            {
                return RedirectToAction("Show", "Project"); 
            }
            else
            {

                return RedirectToAction("IndexError", "Project", new { msg = ar.ErrorMsg });
            }
            
        }
        [HttpGet]

        public ActionResult DeleteEstimate(int pid, int gid, int gl)

        {
            ViewBag.pid = pid;
            ViewBag.gid = gid;
            ViewBag.gl = gl;

            return View();
        }
        [HttpGet]

        public ActionResult ErrorView(string message)

        {
            ErrorModel em = new ErrorModel();

            // em.ErrorMessage = "Database Connection Error";
            em.ErrorMessage = message;

            return View(em);
        }
    }
}