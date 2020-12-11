using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Permissions;
using System.Web;

namespace Assesment.Models
{
    [Table("GATEVERSION")]
    public class GateVersion
    {
        [Key]
        [Column("GID")]
        public int gvId { get; set; }
        [Column("Gate ProjectId")]

        public int gvProjectId { get; set; }
        [Column("Gate Id")]
        public int gvGateId { get; set; }
        [Column("Gate Comments")]

        [DisplayName("Gate Commenets")]
        public string gvComments { get; set; }
        [Column("Gate Assumption")]
        [DisplayName("Gate Assumption")]
        public string gvAssumption { get; set; }
        [Column("Gate Status")]
        [DisplayName("Gate Status")]
        public string gvStatus { get; set; }
        [Column("Gate Line")]
        public int gvLine { get; set; }

         
    }
}

