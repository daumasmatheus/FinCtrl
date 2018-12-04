using FinCtrl.Domain.Entities;
using FinCtrl.Infrastructure.EntityConfigurations;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace FinCtrl.Infrastructure.Contexts
{
    public class FinCtrlDbContext : IdentityDbContext<ApplicationUser>
    {
        public FinCtrlDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static FinCtrlDbContext Create()
        {
            return new FinCtrlDbContext();
        }

        public DbSet<Financa> Financas { get; set; }
        public DbSet<Tipo> Tipos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new FinancaEntityConfiguration());

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}