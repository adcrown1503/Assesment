using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Assesment.Models
{
    [Table("TEAMNAME")]
    public class Team
    {
        [Key]
        [Column("TEAM_ID")]
        public int teamId { get; set; }
        [Column("TEAM_NAME")]
        public string teamName { get; set; }
        [Column("MANAGER_NAME")]
        public string managerName { get; set; }
        [Column("LOGFILE_NAME")]
        public string logFileName { get; set; }
    }
}

