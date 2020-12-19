using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Assesment.Models
{
    public class OracleDbContext: DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("ART");
            
        }
        public DbSet<Application> Applications { get; set; }
    }
}