using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Assesment.Models
{
    public class OracleManager : IProject
    {
        public bool AddNewProject(WrapperUpdateProject wup)
        {
            throw new NotImplementedException();
        }

        public ActionResponse GateRemove(int pid, int gid, int gl)
        {
            throw new NotImplementedException();
        }

        public ActionResponse GateSaveAs(int projectid, int gateid, int line, int newgate)
        {
            throw new NotImplementedException();
        }

        public List<Application> GetApplications()
        {
            OracleDbContext db = new OracleDbContext();
            List<Application> applist = db.Applications.OrderByDescending(f => f.appid).ToList();
            return applist;
        }

        public WrapperViewNewProject GetViewForNewProject()
        {
            throw new NotImplementedException();
        }

        public WrapperViewEditProject GetViewForUpdateGate(int projectId, int gateId, int gateLine)
        {
            throw new NotImplementedException();
        }

        public WrapperViewEditProject GetViewForUpdateProject(int id)
        {
            throw new NotImplementedException();
        }

        public bool SaveProjectInfo(WrapperViewEditProject wvep)
        {
            throw new NotImplementedException();
        }

        public List<Project> ShowProjects()
        {
            throw new NotImplementedException();
        }

        public ActionResponse UpdateApplication(Application app, string saveasnew)
        {
            ActionResponse ar = new ActionResponse();
            List<Application> applist = null;
            OracleDbContext db = new OracleDbContext();

            try
            {
                

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

                MyLogger.GetInstance().Info("Application Updated Successfully");
                ar.IsSuccess = true;
              
            }
            catch (Exception ex)
            {
                ar.IsSuccess = false;
                MyLogger.GetInstance().Error("Error - adding application");
                MyLogger.GetInstance().Error("--------------------------");
                MyLogger.GetInstance().Error(ex.Message);

            }
            applist = db.Applications.OrderByDescending(f => f.appid).ToList();

            ar.obj = applist;
            return ar;
        }

        public ActionResponse UpdateGateInfo(WrapperUpdateProject wup)
        {
            throw new NotImplementedException();
        }
    }
}