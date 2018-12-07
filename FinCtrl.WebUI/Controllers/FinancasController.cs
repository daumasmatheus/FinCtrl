using FinCtrl.Application.Interfaces.Financas;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace FinCtrl.WebUI.Controllers
{
    public class FinancasController : Controller
    {
        private readonly IFinancasServices _financasServices;

        public FinancasController(IFinancasServices financasServices)
        {
            _financasServices = financasServices;
        }

        // GET: Financas
        public ActionResult Index()
        {
            var loggedUserId = User.Identity.GetUserId();

            var result = _financasServices.GetFinancas().Where(x => x.UserId == loggedUserId);

            return View(result);
        }
    }
}