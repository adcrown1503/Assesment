using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Assesment.Models
{
    [Table("ESTIMATE")]
    public class Estimate
       
    {
        [Key]
        [Column("EST_ID")]
        public  int estId { get; set; }
        [Column("PROJECT_FUNCTION_ID")]
        public int projectFunctionId { get; set; }

        [Column("EST_DAYS")]
        public int estDays { get; set; }        
        
        [Column("EST_PHASECODE")]
        public int phaseCode { get; set; }

        [Column("EST_USER")]
        public string userEstimate { get; set; }

        [Column("EST_UPDATEON")]
        public DateTime estimatedOn { get; set; }

        [Column("EST_ROLEID")]        
        public int roleId { get; set; }

        [NotMapped]
        public int ProjFuncId { get; set; }
        [NotMapped]
        public int GateversionId { get; set; }
        [NotMapped]
        public int FuncId { get; set; }
        [NotMapped]
        public string FuncName { get; set; }
        [NotMapped]
        public int AppId { get; set; }
        [NotMapped]
        public int RelId { get; set; }
        [NotMapped]
        public string DeletRows { get; set; }
        [NotMapped]
        public string Flag { get; set; }
    }
}

