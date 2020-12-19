using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

using System.Linq;
using System.Web;

namespace Assesment.Models
{
    [Table("APPLICATION")]
    //[Table("APPLICATION", Schema= "ART")]
    public class Application
    {
        [Key]
        [Column("APP_ID")]
        public int appid { get; set; }
        [Column("APP_NAME")]
        public string appname { get; set; }

    }
}