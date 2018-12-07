using FinCtrl.Application.Interfaces.Financas;
using FinCtrl.Application.Interfaces.Tipos;
using FinCtrl.Domain.Entities;
using FinCtrl.WebUI.ViewModels;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace FinCtrl.WebUI.Controllers
{
    public class FinancasController : Controller
    {
        private readonly IFinancasServices _financasServices;
        private readonly ITiposService _tiposService;

        public FinancasController(IFinancasServices financasServices,
                                  ITiposService tiposService)
        {
            _financasServices = financasServices;
            _tiposService = tiposService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var loggedUserId = User.Identity.GetUserId();

            var result = _financasServices.GetFinancas().Where(x => x.UserId == loggedUserId);

            return View(result);
        }

        [HttpGet]
        public ActionResult Details(string Id)
        {
            var financasDetail = _financasServices.Find(Id);

            return PartialView(financasDetail);
        }

        [HttpGet]
        public ActionResult AddFinanca()
        {
            ViewBag.tipos = new SelectList(_tiposService.GetTipos(), "Id", "Nome");

            return View();
        }

        [HttpPost]
        public ActionResult AddFinanca(FinancaViewModel financaViewModel)
        {
            if (ModelState.IsValid)
            {
                Financa newFinanca = new Financa()
                {
                    Nome = financaViewModel.Nome,
                    DataEntrada = financaViewModel.DataEntrada,
                    Observacao = financaViewModel.Observacao,
                    TipoId = financaViewModel.TipoId,
                    Valor = financaViewModel.Valor,
                    UserId = User.Identity.GetUserId()
                };

                _financasServices.Add(newFinanca);

                return RedirectToAction(nameof(Index));
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [HttpGet]
        public ActionResult EditFinanca(string financaId)
        {
            ViewBag.tipos = new SelectList(_tiposService.GetTipos(), "Id", "Nome");

            var userId = User.Identity.GetUserId();

            var financaToEdit = _financasServices.Find(x => x.Id == financaId && x.UserId == userId);

            return View(financaToEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditFinanca(FinancaViewModel financaViewModel)
        {
            if (ModelState.IsValid)
            {
                Financa financaToEdit = new Financa()
                {
                    Nome = financaViewModel.Nome,
                    DataEntrada = financaViewModel.DataEntrada,
                    Observacao = financaViewModel.Observacao,
                    TipoId = financaViewModel.TipoId,
                    Valor = financaViewModel.Valor
                };

                _financasServices.Edit(financaToEdit);

                return RedirectToAction(nameof(Index));
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [HttpGet]
        public ActionResult DeleteFinanca(string financaId)
        {
            var userId = User.Identity.GetUserId();

            var financaToDelete = _financasServices.Find(x => x.Id == financaId && x.UserId == userId);

            return View(financaToDelete);
        }
    }
}