using Assesment.Controllers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Assesment.Models
{
    public class DatabaseContext:DbContext
    {
        public DbSet<Application> Applications  { get; set; }
        public DbSet<Release> Releases { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<GateVersion> GateVersions { get; set; }
        public DbSet<Functionality> Functionalities { get; set; }
        public DbSet<ProjectFunction> ProjectFunctions { get; set; }
        public DbSet<Estimate> Estimates { get; set; }
        public DbSet<EstimateAttachment> EstimateAttachments { get; set; }
        public DbSet<Gate> Gates { get; set; }
        public DbSet<Phase> Phases { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<ProjectStatus> ProjectStatuses { get; set; }
        public DbSet<DbViewClass> gridClasses { get; set; }
        public DbSet<User> Users { get; set; }
      
    }
}