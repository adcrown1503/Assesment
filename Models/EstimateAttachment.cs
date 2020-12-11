using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Assesment.Models
{
    [Table("ESTIMATE_ATTACHMENTS")]
    public class EstimateAttachment
    {
        [Key]
        [Column("ATT_ID")]
        public int attacheId { get; set; }
        [Column("ATT_FILENAME")]
        public string fileName { get; set; }
        [Column("ATT_GATE_ID")]
        public int gateId { get; set; }
        [Column("ATT_PROJECT_ID")]
        public int projectId { get; set; }
        [Column("ATT_LINE")]
        public int lineNo { get; set; }
    }
}

