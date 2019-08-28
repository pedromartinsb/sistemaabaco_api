using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SistemaVendas.Models;

namespace SistemaVendas.Controllers
{
    public class ColaboradorController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.ListaColaboradores = new ColaboradorModel().ListarTodosColaboradores();
            return View();
        }

        [HttpGet]
        public IActionResult Cadastro(int? id)
        {
            if (id != null)
            {
                // Carrega o registro do Colaborador numa ViewBag
                ViewBag.Colaborador = new ColaboradorModel().RetornarColaborador(id);
            }

            ViewBag.ListaEmpresas = new ColaboradorModel().RetornarListaEmpresas();
            return View();
        }

        [HttpPost]
        public IActionResult Cadastro(ColaboradorModel colaborador)
        {
            if (ModelState.IsValid)
            {
                colaborador.Gravar();
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Excluir(int id)
        {
            ViewData["IdExcluir"] = id;
            return View();
        }

        public IActionResult ExcluirColaborador(int id)
        {
            new ColaboradorModel().Excluir(id);
            return View();
        }
    }
}