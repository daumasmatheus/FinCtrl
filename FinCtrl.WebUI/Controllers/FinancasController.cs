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
    [Authorize]
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

            FinancaViewModel finVm = new FinancaViewModel()
            {
                Id = financasDetail.Id,
                Nome = financasDetail.Nome,
                DataEntrada = financasDetail.DataEntrada,
                Observacao = financasDetail.Observacao,             
                Valor = financasDetail.Valor,
                TipoId = financasDetail.TipoId
            };

            return PartialView(finVm);
        }

        [HttpGet]
        public ActionResult AddFinanca()
        {
            ViewBag.tipos = new SelectList(_tiposService.GetTipos(), "Id", "Nome");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
        public ActionResult EditFinanca(string id)
        {
            ViewBag.tipos = new SelectList(_tiposService.GetTipos(), "Id", "Nome");

            var userId = User.Identity.GetUserId();
            var financaToEdit = _financasServices.Find(x => x.Id == id && x.UserId == userId);

            if (financaToEdit == null)
            {
                return HttpNotFound();
            }

            FinancaViewModel finVm = new FinancaViewModel()
            {
                Id = financaToEdit.Id,
                Nome = financaToEdit.Nome,
                DataEntrada = financaToEdit.DataEntrada,
                Observacao = financaToEdit.Observacao,
                TipoId = financaToEdit.TipoId,
                UserId = financaToEdit.UserId,
                Valor = financaToEdit.Valor
            };

            return View(finVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditFinanca(FinancaViewModel financaViewModel)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();

                Financa fin = new Financa()
                {
                    Id = financaViewModel.Id,
                    Nome = financaViewModel.Nome,
                    DataEntrada = financaViewModel.DataEntrada,
                    Observacao = financaViewModel.Observacao,
                    TipoId = financaViewModel.TipoId,
                    UserId = userId,
                    Valor = financaViewModel.Valor
                };

                _financasServices.Edit(fin);

                return RedirectToAction(nameof(Index));
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [HttpPost]
        public ActionResult DeleteFinanca(string id)
        {
            var userId = User.Identity.GetUserId();

            var financaToDelete = _financasServices.Find(x => x.Id == id && x.UserId == userId);

            _financasServices.Delete(financaToDelete.Id);

            return RedirectToAction(nameof(Index));
        }
    }
}