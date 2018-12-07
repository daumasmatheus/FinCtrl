namespace FinCtrl.Infrastructure.Migrations
{
    using FinCtrl.Domain.Entities;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<FinCtrl.Infrastructure.Contexts.FinCtrlDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FinCtrl.Infrastructure.Contexts.FinCtrlDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //            

            IList<Tipo> defaultTipos = new List<Tipo>();
            defaultTipos.Add(new Tipo() { Id = 1, Nome = "Despesa" });
            defaultTipos.Add(new Tipo() { Id = 2, Nome = "Rendimento" });

            context.Tipos.AddRange(defaultTipos);

            base.Seed(context);
        }
    }
}
