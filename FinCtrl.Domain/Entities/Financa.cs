using System;

namespace FinCtrl.Domain.Entities
{
    public class Financa
    {
        public Financa()
        {
            Id = new Guid().ToString().Replace("-","").Substring(0,10).ToUpper();
        }

        public string Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataEntrada { get; set; }
        public string Observacao { get; set; }
        public decimal Valor { get; set; }
        public int TipoId { get; set; }
        public string UserId { get; set; }

        public virtual Tipo Tipo { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
