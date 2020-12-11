using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assesment.Models
{
    public class WrapperViewEditProject
    {
        public Project project { get; set; }
        public GateVersion gateversion { get; set; }
        public List<GateList> projectGateList { get; set; }
        public List<DbViewClass> dbview { get; set; }
        public List<GroupFunction> groupFunction { get; set; }
        public List<Phase> phaselist { get; set; }
        public List<Role> roleList { get; set; }
        public List<Application> appList { get; set; }
        public List<Release> relList { get; set; }
        public List<Gate> gateList { get; set; }
        public List<ProjectStatus> statusList { get; set; }
        public List<Functionality> functionList { get; set; }

        public List<EstimateAttachment> attachments { get; set; }
        //public string impactType { get; set; }
        //public int statusId { get; set; }
        //public int gvGateId { get; set; }
        //public string gvStatus { get; set; }
        //public int FuncId { get; set; }
    }
}