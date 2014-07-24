using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace EmployementApplication.Models
{
    public class ManagementDbContext :DbContext
    {
        public ManagementDbContext() : base("Management")
        {
           Database.SetInitializer<EmployementApplication.Models.ManagementDbContext>(null);
        }
        public DbSet<Employees> Employs { get; set; }
        public DbSet<Departments> Departments { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
       
    }
}