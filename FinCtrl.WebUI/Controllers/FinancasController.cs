using FinCtrl.Application.Interfaces.Financas;
using FinCtrl.Application.Interfaces.Tipos;
using FinCtrl.Domain.Entities;
using FinCtrl.WebUI.ViewModels;
using Highsoft.Web.Mvc.Charts;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
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
            ViewBag.tipos = new SelectList(_tiposService.GetTipos(), "Id", "Nome");
            return View(financaViewModel);
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

            ViewBag.tipos = new SelectList(_tiposService.GetTipos(), "Id", "Nome");
            return View(financaViewModel);
        }

        [HttpPost]
        public ActionResult DeleteFinanca(string id)
        {
            var userId = User.Identity.GetUserId();

            var financaToDelete = _financasServices.Find(x => x.Id == id && x.UserId == userId);

            _financasServices.Delete(financaToDelete.Id);

            return RedirectToAction(nameof(Index));
        }
        
        [HttpGet]
        public ActionResult Relatorios()
        {
            var financasPorAno = _financasServices.GetFinancas().Where(d => d.DataEntrada.Year == 2018);

            double totalRendimentos = financasPorAno.Where(t => t.TipoId == 2).Select(v => Convert.ToDouble(v.Valor)).Sum();
            double totalDespesas = financasPorAno.Where(t => t.TipoId == 1).Select(v => Convert.ToDouble(v.Valor)).Sum();

            List<PieSeriesData> categoryData = new List<PieSeriesData>();
            categoryData.Add(new PieSeriesData { Name = "Despesas",
                                                 Y = totalDespesas,
                                                 Drilldown = "DespesasDetalhes",
                                                 Color = "rgba(168, 250, 171, 1)"
            });

            categoryData.Add(new PieSeriesData { Name = "Rendimentos",
                                                 Y = totalRendimentos,
                                                 Drilldown = "RendimentosDetalhes",
                                                 Color = "rgba(150, 8, 8, 1)"
            });

            List<PieSeriesData> pieDespesasData = new List<PieSeriesData>();
            foreach (var item in financasPorAno.Where(t => t.TipoId == 1))
            {
                pieDespesasData.Add(new PieSeriesData { Name = item.Nome, Y = Convert.ToDouble(item.Valor) });
            }

            List<PieSeriesData> pieRendimentosData = new List<PieSeriesData>();
            foreach(var item in financasPorAno.Where(t => t.TipoId == 2))
            {
                pieRendimentosData.Add(new PieSeriesData { Name = item.Nome, Y = Convert.ToDouble(item.Valor) });
            }


            ViewData["categoryData"] = categoryData;

            ViewData["despesasData"] = pieDespesasData;
            ViewData["rendimentosData"] = pieRendimentosData;

            return View();
        }
    }
}