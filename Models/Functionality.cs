using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Assesment.Models
{
    [Table("[FUNCTIONALITY]")]
    public class Functionality
    {
        [Key]
        [Column("FUNC_ID")]
        public int FuncId { get; set; }
        [Column("FUNC_NAME")]
        public string FuncName { get; set; }
    }
}

