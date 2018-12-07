using System;
using System.ComponentModel.DataAnnotations;

namespace FinCtrl.WebUI.ViewModels
{
    public class FinancaViewModel
    {
        [Required(ErrorMessage = "Informe o titulo da finança.")]
        [MaxLength(255, ErrorMessage = "Limite de 255 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe a data da entrada do registro.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataEntrada { get; set; }

        [MaxLength(25555, ErrorMessage = "Limite de 25.555 caracteres.")]
        public string Observacao { get; set; }

        [Required(ErrorMessage = "Informe o valor.")]
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "Informe o tipo da financa: Despesa ou Rendimento.")]
        public int TipoId { get; set; }
    }
}