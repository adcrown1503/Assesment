using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Assesment.Models
{
    [Table("ROLE")]
    public class Role
    {
        [Key]
        [Column("ROLE_ID")]
        public int roleId { get; set; }
        [Column("ROLE_DESCRIPTION")]
        public string roleDesc { get; set; }
        [Column("ROLE_NAME")]
        public string roleName { get; set; }
        [Column("ROLE_ORDER")]
        public int roleOrder { get; set; }
    }
}

