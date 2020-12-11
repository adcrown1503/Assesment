using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Assesment.Models
{
    [Table("V01")]
    public class DbViewClass
    {
        [Column("PROJ_ID")]
        public int id { get; set; }
        [Column("PROJ_NAME")]
        public string name { get; set; }
        [Column("PROJ_ASSESMENT_DATE")]
        public DateTime assesmentDate { get; set; }
        [Column("PROJ_DESIRED_REL_DATE")]
        public DateTime desiredReleaseDate { get; set; }
        [Column("PROJ_IMPACT_TYPE")]
        public string impactType { get; set; }
        [Column("PROJ_ASUMPTIONS")]
        public string assumption { get; set; }
        [Column("PROJ_COMMETNS")]
        public string comments { get; set; }
        [Column("PROJ_STATUS_ID")]
        public int statusId { get; set; }

        [Column("GID")]
        public int gvId { get; set; }
        [Column("Gate ProjectId")]
        public int gvProjectId { get; set; }
        [Column("Gate Id")]
        public int gvGateId { get; set; }
        [Column("GATE_DESCRIPTION")]
        public string gateDesc { get; set; }
        [Column("Gate Comments")]
        public string gvComments { get; set; }
        [Column("Gate Assumption")]
        public string gvAssumption { get; set; }
        [Column("Gate Status")]
        public string gvStatus { get; set; }
        [Column("Gate Line")]
        public int gvLine { get; set; }
        [Column("PROJ_FUNC_ID")]
        public int ProjFuncId { get; set; }
        [Column("FUNC_ID")]
        public int FuncId { get; set; }
        [Column("APP_ID")]
        public int AppId { get; set; }
        [Column("REL_ID")]
        public int RelId { get; set; }
        [Column("EST_ID")]
        public int estId { get; set; }

        [Column("EST_DAYS")]
        public int estDays { get; set; }

        [Column("EST_PHASECODE")]
        public int phaseCode { get; set; }

        [Column("EST_ROLEID")]
        public int roleId { get; set; }
        [Column("FUNC_NAME")]
        public string FuncName { get; set; }
    }
}