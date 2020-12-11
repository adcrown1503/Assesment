using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Assesment.Models
{
    [Table("PROJECT_FUNCTIONS")]
    public class ProjectFunction
    {
        [Key]
        [Column("PROJ_FUNC_ID")]
        public int ProjFuncId { get; set; }
        [Column("GATEVERSIONID")]
        public int GateversionId { get; set; }
        [Column("FUNC_ID")]
        public int FuncId { get; set; }
        [Column("APP_ID")]
        public int AppId { get; set; }
        [Column("REL_ID")]
        public int RelId { get; set; }

       
    }
}