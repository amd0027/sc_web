using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace sc_web.DataLayer
{
    public class PairingOperationsContext : DbContext
    {

        public PairingOperationsContext() : base("PairingOperationsContext")
        {
        }

        public DbSet<Models.PairingOperation> PairingOperations { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}