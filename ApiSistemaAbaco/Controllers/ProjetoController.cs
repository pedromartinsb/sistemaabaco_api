using System;
using System.Net;
using System.Collections.Generic;
using ApiSistemaAbaco.Repositorio;
using Microsoft.AspNetCore.Mvc;
using ApiSistemaAbaco.Models;

namespace ApiSistemaAbaco.Controllers
{
    [Route("api/[Controller]")]    
    public class ProjetoController : Controller
    {
        private readonly IProjetoRepository _projetoRepository;
        
        public ProjetoController(IProjetoRepository projetoRepo)
        {
            _projetoRepository = projetoRepo;
        }

        [HttpGet]
        public IEnumerable<Projeto> GetAll()
        {
            return _projetoRepository.GetAll();    
        }

        [HttpGet("{id}", Name="GetProjeto")]
        public IActionResult GetById(long id)
        {
            var projeto = _projetoRepository.Find(id);
            if(projeto == null)
                return NotFound();

            return new ObjectResult(projeto);    
        }

        [HttpPost]
        public IActionResult Create([FromBody] Projeto projeto)
        {
            if(projeto == null)
                return BadRequest();
            
            _projetoRepository.Add(projeto);

            return CreatedAtRoute("GetProjeto", new {id = projeto.Id}, projeto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Projeto projeto)
        {
            if(projeto == null || projeto.Id != id)
                return BadRequest();

            var _projeto = _projetoRepository.Find(id);

            if(_projeto == null)
                return NotFound();

            _projeto.Nome = projeto.Nome;
            _projeto.Descricao = projeto.Descricao;
            _projeto.Data = DateTime.Now;

            _projetoRepository.Update(_projeto);
            return new NoContentResult();
        }
    }
}