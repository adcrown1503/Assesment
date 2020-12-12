using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Assesment.Models
{
    public class ProjectManager : IProject
    {
        public bool AddNewProject(WrapperUpdateProject model)
        {
            string msg = "";
            bool output = false;
            using (DatabaseContext db = new DatabaseContext())
            {
                using (DbContextTransaction transaction = db.Database.BeginTransaction())
                {

                    try
                    {
                        db.Projects.Add(model.project);

                        db.SaveChanges();

                        model.gateVersion.gvProjectId = model.project.id;

                        db.GateVersions.Add(model.gateVersion);

                        db.SaveChanges();

                        var result = model.estimate.GroupBy(x => new { x.FuncId, x.AppId, x.RelId });

                        foreach (var item in result)
                        {
                            // System.Diagnostics.Debug.WriteLine("{0} - {1}", item.Key, item.Count());
                            //   System.Diagnostics.Debug.WriteLine("{0} - {1} - {2}",item.Key.FuncId,item.Key.AppId,item.Key.RelId);
                            ProjectFunction pf = new ProjectFunction();
                            pf.AppId = item.Key.AppId;
                            pf.RelId = item.Key.RelId;
                            pf.FuncId = item.Key.FuncId;
                            pf.GateversionId = model.gateVersion.gvId;
                            db.ProjectFunctions.Add(pf);
                            db.SaveChanges();
                            //    throw new Exception();

                            var resultEstimate = model.estimate.Where(x => x.FuncId == pf.FuncId);
                            foreach (var item2 in resultEstimate)
                            {
                                Estimate est = new Estimate();
                                est.projectFunctionId = pf.ProjFuncId;
                                est.estDays = item2.estDays;
                                est.phaseCode = item2.phaseCode;
                                est.roleId = item2.roleId;
                                est.estimatedOn = item2.estimatedOn;
                                est.userEstimate = item2.userEstimate;
                                db.Estimates.Add(est);
                                db.SaveChanges();

                            }
                        }
                        transaction.Commit();
                        MyLogger.GetInstance().Info("project created successfully");
                        output = true;
                    }
                    catch (Exception ex)
                    {
                        if (ex.InnerException is null)
                        {
                            msg = ex.Message;

                        }
                        else
                        {
                            msg = ex.InnerException.Message;

                        }
                        // System.Diagnostics.Debug.WriteLine(ex.InnerException.Message);
                        // Console.WriteLine(ex.InnerException.Message);
                        // return false;

                        transaction.Rollback();

                        MyLogger.GetInstance().Error("Error  - saving new project" + msg);

                        output = false;

                    }
                }
            }
            return output;
        }
        public WrapperViewNewProject GetViewForNewProject()
        {
            WrapperViewNewProject wp = new WrapperViewNewProject();
            using (DatabaseContext db = new DatabaseContext())
            {

                try
                {
                    List<ProjectStatus> statusList = db.ProjectStatuses.ToList();

                    List<Gate> gateList = db.Gates.ToList();
                    List<Functionality> functionList = db.Functionalities.ToList();
                    List<Phase> phaseList = db.Phases.ToList();
                    List<Role> roleList = db.Roles.ToList();
                    List<Application> appList = db.Applications.ToList();
                    List<Release> releaseList = db.Releases.ToList();
                    wp.functionList = functionList;
                    wp.statusList = statusList;
                    wp.gateList = gateList;
                    wp.phaselist = phaseList;
                    wp.roleList = roleList;
                    wp.appList = appList;
                    wp.relList = releaseList;
                }
                catch (Exception ex)
                {
                    MyLogger.GetInstance().Error("Exception - creating new project " + ex.Message);


                    // return RedirectToAction("IndexError", "Project", new { msg = ex.Message });
                    return null;
                }

            }
            return wp;
        }
        public List<Project> ShowProjects()
        {
            List<Project> plist = null;
            using (DatabaseContext db = new DatabaseContext())
            {

                plist = db.Projects.ToList();

            }
            return plist;
        }
        public WrapperViewEditProject GetViewForUpdateProject(int id)
        {
            WrapperViewEditProject wp = new WrapperViewEditProject();
            string msg = "";
            using (DatabaseContext db = new DatabaseContext())
            {
                try
                {
                    Project project = db.Projects.SingleOrDefault(a => a.id == id);
                    List<Gate> gateList = db.Gates.ToList();
                    List<GateVersion> gateVersions = db.GateVersions.Where(a => a.gvProjectId == id).ToList();

                    var result = gateVersions.Join(gateList, gv => gv.gvGateId, g => g.gateId, (a, b) => new
                    {
                        projectId = project.id,
                        gateId = a.gvGateId,
                        gateDesc = b.gateDesc,
                        gateLine = a.gvLine
                    });

                    List<GateList> projectGateList = new List<GateList>();

                    foreach (var item in result)
                    {
                        GateList gl = new GateList();
                        gl.gateDesc = item.gateDesc;
                        gl.gateId = item.gateId;
                        gl.gateLine = item.gateLine;
                        gl.projectId = item.projectId;

                        projectGateList.Add(gl);


                    }


                    List<ProjectStatus> statusList = db.ProjectStatuses.ToList();


                    wp.project = project;
                    wp.statusList = statusList;
                    wp.projectGateList = projectGateList;
                }
                catch (Exception ex)
                {
                    if (ex.InnerException is null)
                    {
                        msg = ex.Message;

                    }
                    else
                    {
                        msg = ex.InnerException.Message;

                    }

                    MyLogger.GetInstance().Error("Error  - Editing Project" + msg);
                }
                return wp;
            }
        }

        public bool SaveProjectInfo(WrapperViewEditProject wvep)
        {
            string msg = "";
            bool output = false;
            using (DatabaseContext db = new DatabaseContext())
            {
                using (DbContextTransaction transaction = db.Database.BeginTransaction())
                {

                    try
                    {
                        Project prj = new Project();
                        prj = wvep.project;
                        db.Projects.Add(prj);
                        db.Entry(prj).State = EntityState.Modified;
                        db.SaveChanges();

                        msg = "Sucessfully updated";

                        transaction.Commit();
                        output = true;
                    }
                    catch (Exception ex)
                    {
                        if (ex.InnerException is null)
                        {
                            msg = ex.Message;

                        }
                        else
                        {
                            msg = ex.InnerException.Message;

                        }
                        MyLogger.GetInstance().Error("Error - Udating Project Info" + msg);


                        transaction.Rollback();



                    }

                }
                return output;
            }
        }

        public WrapperViewEditProject GetViewForUpdateGate(int projectId, int gateId, int gateLine)
        {
            string msg = "";
            WrapperViewEditProject wp = new WrapperViewEditProject();

            Project p = new Project();
            GateVersion gv = new GateVersion();

            wp.project = p;
            wp.gateversion = gv;
            using (DatabaseContext db = new DatabaseContext())
            {
                //  ArrayList dayslist = new ArrayList();
                List<GroupFunction> dayslist = new List<GroupFunction>();
                try
                {
                    List<DbViewClass> dbview = db.gridClasses.AsNoTracking()
                        .Where(n => n.id == projectId && n.gvGateId == gateId && n.gvLine == gateLine)
                        .OrderBy(n => n.ProjFuncId).
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
                    foreach (var aa in dbview)
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


                    List<EstimateAttachment> attachments = db.EstimateAttachments.Where(x => x.projectId == projectId
                    && x.gateId == gateId && x.lineNo == gateLine).ToList();

                    List<Phase> phaseList = db.Phases.ToList();  //int projectId, int gateId, int gateLine
                    List<Role> roleList = db.Roles.ToList();
                    List<Application> appList = db.Applications.ToList();
                    List<Release> releaseList = db.Releases.ToList();
                    List<Functionality> functionList = db.Functionalities.ToList();
                    List<ProjectStatus> statusList = db.ProjectStatuses.ToList();
                    List<Gate> gateList = db.Gates.ToList();


                    //   

                    wp.attachments = attachments;
                    wp.groupFunction = dayslist;
                    wp.project.impactType = dbview[0].impactType;
                    wp.project.statusId = dbview[0].statusId;
                    wp.gateversion.gvGateId = dbview[0].gvGateId;
                    wp.gateversion.gvStatus = dbview[0].gvStatus;
                    //wp.funcid= gridClassList[0].FuncId;

                    wp.dbview = dbview;
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

                    if (ex.InnerException is null)
                    {
                        msg = ex.Message;

                    }
                    else
                    {
                        msg = ex.InnerException.Message;

                    }


                    msg = msg + "\n" + "Error - getting gate details..Project Id = " + projectId + " Gate Id = " + gateId + " Line = " + gateLine;

                    MyLogger.GetInstance().Error(msg);


                }

            }
            return wp;



        }
        public ActionResponse UpdateGateInfo(WrapperUpdateProject model)
        {
            //bool output = false;
            ActionResponse ar = new ActionResponse();

            if (model.estimate != null)
            {


                using (DatabaseContext db = new DatabaseContext())
                {

                    using (DbContextTransaction transaction = db.Database.BeginTransaction())
                    {
                        try
                        {
                            //Delete Functionality

                            string listRecDel = model.estimate[0].DeletRows;

                            if (listRecDel != null)
                            {
                                string[] ids = listRecDel.Split(',');

                                for (int i = 0; i < ids.Length; i++)
                                {
                                    int recid = Convert.ToInt32(ids[i]);

                                    List<Estimate> deleteList = db.Estimates.Where(id => id.projectFunctionId == recid).ToList();
                                    foreach (Estimate e in deleteList)
                                    {
                                        db.Entry(e).State = EntityState.Deleted;
                                        db.SaveChanges();
                                    }
                                    ProjectFunction pf = db.ProjectFunctions.SingleOrDefault(id => id.ProjFuncId == recid);
                                    db.Entry(pf).State = EntityState.Deleted;
                                    db.SaveChanges();
                                }

                            }


                            GateVersion gv = model.gateVersion;
                            // gv.gvId = model.gateVersion.gvId;
                            db.Entry(gv).State = EntityState.Modified;
                            db.SaveChanges();

                            // List<Estimate> NewEstimate = model.estimate.Where(o => o.projectFunctionId == 0).ToList();

                            List<Estimate> NewGroupEstimate = model.estimate.GroupBy(x => new { x.ProjFuncId, x.GateversionId, x.FuncId, x.AppId, x.RelId })
                                .Select(o => o.First()).ToList().Select(o => new Estimate
                                {
                                    ProjFuncId = o.ProjFuncId,
                                    GateversionId = o.GateversionId,
                                    FuncId = o.FuncId,
                                    AppId = o.AppId,
                                    RelId = o.RelId
                                }).ToList();

                            foreach (Estimate e in NewGroupEstimate)
                            {
                                ProjectFunction pf = new ProjectFunction();
                                pf.ProjFuncId = e.ProjFuncId;
                                pf.AppId = e.AppId;
                                pf.RelId = e.RelId;
                                pf.FuncId = e.FuncId;
                                pf.GateversionId = e.GateversionId;
                                db.ProjectFunctions.Add(pf);

                                if (pf.ProjFuncId > 0)
                                {

                                    db.Entry(pf).State = EntityState.Modified;
                                }


                                db.SaveChanges();

                                var resultEstimate = model.estimate.Where(x => x.FuncId == pf.FuncId);
                                foreach (var item2 in resultEstimate)
                                {
                                    if (item2.Flag != "X")
                                    {
                                        Estimate est = new Estimate();
                                        est.projectFunctionId = pf.ProjFuncId;
                                        est.estId = item2.estId;
                                        est.estDays = item2.estDays;
                                        est.phaseCode = item2.phaseCode;
                                        est.roleId = item2.roleId;
                                        est.estimatedOn = item2.estimatedOn;
                                        est.userEstimate = item2.userEstimate;
                                        db.Estimates.Add(est);
                                        if (est.estId > 0)
                                        {
                                            if (item2.Flag == "D")
                                            {
                                                db.Entry(est).State = EntityState.Deleted;
                                            }
                                            else
                                            {
                                                db.Entry(est).State = EntityState.Modified;
                                            }

                                        }
                                        db.SaveChanges();

                                    }

                                }

                            }


                            transaction.Commit();

                            MyLogger.GetInstance().Info("gate info updated successfully.." + model.gateVersion.gvProjectId);

                            ar.IsSuccess = true;
                        }
                        catch (Exception ex)
                        {
                            MyLogger.GetInstance().Error("Error - updating gate info " + model.gateVersion.gvProjectId);
                            MyLogger.GetInstance().Error("Error - updating gate info " + ex.Message);

                            transaction.Rollback();
                            ar.IsSuccess = false;
                            ar.ErrorMsg = "Database exception";
                        };

                    }

                }
            }
            else
            {
                ar.IsSuccess = false;
                ar.ErrorMsg = "Model invalid";
            }

            return ar;
        }
        public ActionResponse GateSaveAs(int projectid, int gateid, int line, int newgate)
        {
            ActionResponse ar = new ActionResponse();
            using (DatabaseContext db = new DatabaseContext())
            {
                try
                {

                    var result = db.Database.ExecuteSqlCommand("copygate @projid, @oldGateId,@oldlineNo,@newGateId",
                        new SqlParameter("@projId", projectid),
                        new SqlParameter("@oldGateId", gateid),
                        new SqlParameter("@oldlineNo", line),
                        new SqlParameter("@newGateId", newgate));

                    MyLogger.GetInstance().Info("gate copied successfully ");
                    ar.IsSuccess = true;
                }
                catch (Exception ex)
                {

                    MyLogger.GetInstance().Error("Error - gate copy ");
                    MyLogger.GetInstance().Error("Error - gate copy " + ex.Message);
                    ar.IsSuccess = false;
                    ar.ErrorMsg = ex.Message;
                }


            };

            return ar;

        }
        public ActionResponse GateRemove(int pid, int gid, int gl)
        {
            ActionResponse ar = new ActionResponse();

            using (DatabaseContext db = new DatabaseContext())
            {
                using (DbContextTransaction transaction = db.Database.BeginTransaction())
                {
                    try
                    {

                        GateVersion gv = db.GateVersions.Where(x => x.gvProjectId == pid && x.gvGateId == gid && x.gvLine == gl).First();
                        List<ProjectFunction> listfunc = db.ProjectFunctions.Where(x => x.GateversionId == gv.gvId).ToList();
                        foreach (var item in listfunc)
                        {
                            List<Estimate> listEstimate = db.Estimates.Where(x => x.projectFunctionId == item.ProjFuncId).ToList();
                            foreach (var obj in listEstimate)
                            {
                                db.Entry(obj).State = EntityState.Deleted;
                                db.SaveChanges();
                            }
                            db.Entry(item).State = EntityState.Deleted;
                            db.SaveChanges();
                        }
                        db.Entry(gv).State = EntityState.Deleted;
                        db.SaveChanges();

                        List<EstimateAttachment> listattachmets = db.EstimateAttachments.
                            Where(x => x.projectId == pid && x.gateId == gid && x.lineNo == gl).ToList();
                        foreach (var item in listattachmets)
                        {
                            db.Entry(item).State = EntityState.Deleted;
                            db.SaveChanges();
                        }
                        var count = db.GateVersions.Count(t => t.gvProjectId == pid);

                        if (count == 0)
                        {
                            Project prj = db.Projects.Where(x => x.id == pid).First();
                            db.Entry(prj).State = EntityState.Deleted;
                            db.SaveChanges();
                        }
                        transaction.Commit();

                        ar.SuccessMsg = "Gate remove successfully";
                        ar.IsSuccess = true;
                        MyLogger.GetInstance().Info(ar.SuccessMsg);

                        
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();

                        ar.IsSuccess = false;

                        if (ex.InnerException.Message != null)
                        {
                            ar.ErrorMsg = ex.InnerException.Message;
                        }
                        else
                        {
                            ar.ErrorMsg = ex.Message;
                        }
                        MyLogger.GetInstance().Error("Error - Gate removal " + pid);
                        MyLogger.GetInstance().Error(ar.ErrorMsg);
                    }
                }
            }
                return ar;
        }
    } ///////////
}