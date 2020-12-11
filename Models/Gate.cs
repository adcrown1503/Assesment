using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Assesment.Models
{
    [Table("GATES")]
    public class Gate
    {
        [Key]
        [Column("GATE_ID")]
        public int gateId { get; set; }
        [Column("GATE_DESCRIPTION")]
        public string gateDesc { get; set; }
        [Column("GATE_TYPE")]
        public string gateType { get; set; }
        [Column("GATE_LEVEL")]
        public string gateLevel { get; set; }
        
    }
}

