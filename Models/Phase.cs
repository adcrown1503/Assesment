using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Assesment.Models
{
    [Table("PHASE")]
    public class Phase
    {
        [Key]
        [Column("PHASE_ID")]
        public int phaseId { get; set; }
        [Column("PHASE_CODE")]
        public int phaseCode { get; set; }
        [Column("PHASE_DESC")]
        public string phaseDesc { get; set; }
    }
}

