using System.Collections.Generic;

namespace FinCtrl.Domain.Entities
{
    public class Tipo
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public virtual ICollection<Financa> Financas { get; set; }
    }
}