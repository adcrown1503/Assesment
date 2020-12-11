using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assesment.Models
{
    public class WrapperViewNewProject
    {

        //public Project project { get; set; }
        //public GateVersion gateversion { get; set; }
        //public ProjectFunction projectFunction { get; set; }
        //public ProjectStatus projectStatus { get; set; }
        //public Gate gate { get; set; }

        //public Phase phase { get; set; }
        //public Functionality functionality { get; set; }
        //public Role role { get; set; }
        //public Release release { get; set; }
        //public Application application { get; set; }
        public List<ProjectStatus> statusList { get; set; }
        public List<Gate> gateList { get; set; }        
        
        public List<Functionality> functionList { get; set; }
        
        public List<Phase> phaselist { get; set; }
        
        public List<Role> roleList { get; set; }        
        
        public List<Application> appList { get; set; }
        public List<Release> relList { get; set; }
       
        

    }
}