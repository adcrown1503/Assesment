using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assesment.Models
{
    [Table("PROJECT")]
    public class Project
    {
        [Key]
        [Column("PROJ_ID")]
        [DisplayName("Project Id")]
        public int id { get; set; }

        [DisplayName("Project Name")]
        [Column("PROJ_NAME")]
        [Required]
        //[Remote("CheckName", "Project", ErrorMessage = "Project name already exist")]
        public string name { get; set; }

        [DisplayName("Assesment Date")]
        [Column("PROJ_ASSESMENT_DATE")]
        [Required]
        //[DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        //[UIHint("DateTime")]
        public DateTime assesmentDate { get; set; }

        [DisplayName("Release Date")]
        [Column("PROJ_DESIRED_REL_DATE")]
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime desiredReleaseDate { get; set; }
        [DisplayName("Impact Type")]
        [Column("PROJ_IMPACT_TYPE")]
        [Required]
        public string impactType { get; set; }
        [DisplayName("Project Assumption")]
        [Column("PROJ_ASUMPTIONS")]
        [Required]
        public string assumption { get; set; }

        [DisplayName("Project Commenets")]
        [Column("PROJ_COMMETNS")]
        [Required]
        public string comments { get; set; }
        [DisplayName("Project Status")]
        [Column("PROJ_STATUS_ID")]
        [Required]
       
        public int statusId { get; set; }
       
        
     
    }
}

