using System.Collections.Generic;
using System.Linq;
using ApiSistemaAbaco.Models;

namespace ApiSistemaAbaco.Repositorio
{
    public class ProjetoRepository : IProjetoRepository
    {
        private readonly SistemaDbContext _contexto;

        public ProjetoRepository(SistemaDbContext ctx)
        {
            _contexto = ctx;
        }

        public void Add(Projeto projeto)
        {
            _contexto.Projeto.Add(projeto);
            _contexto.SaveChanges();
        }

        public Projeto Find(long id)
        {
            return _contexto.Projeto.FirstOrDefault(u => u.Id == id);
        }

        public IEnumerable<Projeto> GetAll()
        {
            return _contexto.Projeto.ToList();
        }

        public void Remove(long id)
        {
            var entity = _contexto.Projeto.First(u => u.Id == id);
            _contexto.Projeto.Remove(entity);
            _contexto.SaveChanges();
        }

        public void Update(Projeto projeto)
        {
            _contexto.Projeto.Update(projeto);
            _contexto.SaveChanges();
        }
    }
}