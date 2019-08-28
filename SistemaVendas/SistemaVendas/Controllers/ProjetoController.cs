using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaVendas.Models;

namespace SistemaVendas.Controllers
{
    public class ProjetoController : Controller
    {
        private IHttpContextAccessor httpContext;

        public ProjetoController(IHttpContextAccessor httpContextAccessor)
        {
            httpContext = httpContextAccessor;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.ListaProjetos = new ProjetoModel().ListarTodosProjetos();            
            ViewBag.TotalProjetos = ViewBag.ListaProjetos.Count;

            // trazendo projetos por etapa
            ViewBag.PreVenda = new ProjetoModel().RetornarProjetosPorEtapa(1);
            ViewBag.Proposta = new ProjetoModel().RetornarProjetosPorEtapa(2);
            ViewBag.Negociacao = new ProjetoModel().RetornarProjetosPorEtapa(3);
            ViewBag.Fechamento = new ProjetoModel().RetornarProjetosPorEtapa(4);
            ViewBag.Producao = new ProjetoModel().RetornarProjetosPorEtapa(5);
            ViewBag.Entrega = new ProjetoModel().RetornarProjetosPorEtapa(6);

            CarregarDados();
            return View();
        }

        [HttpGet]
        public IActionResult Registrar(int? id)
        {
            if (id != null)
            {
                // Carrega o registro do Cliente numa ViewBag
                ViewBag.Projeto = new ProjetoModel().RetornarProjeto(id);
            }

            CarregarDados();
            return View();
        }

        [HttpPost]
        public IActionResult Registrar(ProjetoModel projeto)
        {
            projeto.Inserir();
            CarregarDados();
            return RedirectToAction("Index", "Projeto");
            //return View();
        }

        private void CarregarDados()
        {
            ViewBag.ListaClientes = new ProjetoModel().RetornarListaClientes();
            ViewBag.ListaColaboradores = new ProjetoModel().RetornarListaColaboradores();
            ViewBag.ListaProdutos = new ProjetoModel().RetornarListaProdutos();
            ViewBag.ListaEtapas = new EtapaModel().ListarTodasEtapas();
            ViewBag.ListaEmpresas = new ProjetoModel().RetornarListaEmpresas();
        }
    }
}