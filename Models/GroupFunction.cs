using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assesment.Models
{
    public class GroupFunction
    {
        public int FuncId { get; set; }
        public int ProjFuncId { get; set; }
        public int appid { get; set; }
        public int releaseId { get; set; }
        public string FuncName { get; set; }

        public int totalDays { get; set; }
        public string daysString { get; set; }
    }
}