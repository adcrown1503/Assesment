using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Assesment.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        [Column("Id")]
        [DisplayName("Id")]
        public int id { get; set; }

        [Required]
        [Column("UserName")]
        [DisplayName("User Name")]
        public string userName { get; set; }
        [Required]
        [Column("UserPassword")]
        [DisplayName("User Password")]
        public string userPassword { get; set; }
    }
}