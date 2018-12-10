using System.ComponentModel.DataAnnotations;

namespace FinCtrl.WebUI.ViewModels
{
    public class TipoViewModel
    {
        [Display(Name = "Tipo de financa")]
        public string Nome { get; set; }
    }
}