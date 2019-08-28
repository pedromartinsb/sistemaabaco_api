using System.Collections.Generic;
using ApiSistemaAbaco.Models;

namespace ApiSistemaAbaco.Repositorio
{
    public interface IProjetoRepository
    {
         void Add(Projeto projeto);
         IEnumerable<Projeto> GetAll();
         Projeto Find(long id);
         void Remove(long id);
         void Update(Projeto projeto);
    }
}