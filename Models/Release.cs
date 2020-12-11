using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Assesment.Models
{
    [Table("RELEASE")]
    public class Release
    {
        [Key]
        [Column("REL_ID")]
        [Required]
        public int releaseId { get; set; }
        [Column("REL_NAME")]
        [Required]
        public string releaseName { get; set; }
        [Column("REL_DATE")]
        public DateTime releaseDate { get; set; }
    }
}