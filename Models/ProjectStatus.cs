using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Assesment.Models
{
    [Table("PROJECT_STATUS")]
    public class ProjectStatus
    {
        [Key]
        [Column("PRJSTS_ID")]
        public int statusId { get; set; }
        [Column("PRJSTS_NAME")]
        public string statusName { get; set; }
    }
}

