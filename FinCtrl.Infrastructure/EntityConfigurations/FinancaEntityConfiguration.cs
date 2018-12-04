using FinCtrl.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace FinCtrl.Infrastructure.EntityConfigurations
{
    public class FinancaEntityConfiguration : EntityTypeConfiguration<Financa>
    {
        public FinancaEntityConfiguration()
        {
            HasKey(f => f.Id);

            Property(f => f.Nome).HasMaxLength(255).IsRequired();
            Property(f => f.Nome).HasMaxLength(25555).IsOptional();
            Property(f => f.Valor).IsRequired();

            HasRequired(t => t.Tipo)
                .WithMany(f => f.Financas)
                .HasForeignKey(t => t.TipoId)
                .WillCascadeOnDelete(false);

            HasRequired(u => u.User)
                .WithMany(f => f.Financas)
                .HasForeignKey(u => u.UserId)
                .WillCascadeOnDelete(false);
        }
    }
}
