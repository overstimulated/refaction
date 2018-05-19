using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace refactor_me.Models
{
    public class MainContext : DbContext
    {
        public MainContext() : base("name=Database")
        {
        }

        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductOption> ProductOption { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}