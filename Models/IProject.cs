﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assesment.Models
{
    public interface IProject
    {
        WrapperViewNewProject GetViewForNewProject();
        bool AddNewProject(WrapperUpdateProject wup);
        List<Project>  ShowProjects();
        WrapperViewEditProject GetViewForUpdateProject(int id);

        bool SaveProjectInfo(WrapperViewEditProject wvep);

        WrapperViewEditProject GetViewForUpdateGate(int projectId, int gateId, int gateLine);
        ActionResponse UpdateGateInfo(WrapperUpdateProject wup);

        ActionResponse GateSaveAs(int projectid, int gateid, int line, int newgate);

        ActionResponse GateRemove(int pid, int gid, int gl);
        ActionResponse UpdateApplication(Application app,string saveasnew);

        List<Application> GetApplications();
    }
}
