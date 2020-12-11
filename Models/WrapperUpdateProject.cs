using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assesment.Models
{
    public class WrapperUpdateProject
    {
        public Project project { get; set; }
        public GateVersion gateVersion { get; set; }
        //public int ProjFuncId { get; set; }
        //public int GateversionId { get; set; }
        //public int FuncId { get; set; }
        //public int AppId { get; set; }
        //public int Rel_id { get; set; }
        public List<Estimate> estimate { get; set; }
    }
}


