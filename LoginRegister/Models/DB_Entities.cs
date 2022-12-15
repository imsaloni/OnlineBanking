using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LoginRegister.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace LoginRegister.Models
{
    public class DB_Entities: DbContext
    {
        #region group4
        public DB_Entities() : base("DatabaseMVC5") { }
       
        public DbSet<AccountDetails>AccountDetails { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Database.SetInitializer<demoEntities>(null);
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        #endregion
    }
}
    