using System;
namespace ApiSistemaAbaco.Models
{
    public class Projeto
    {
        public int Id { get; set; }
        public int Id_Cliente { get; set; }
        public int Id_Etapa { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
        public decimal Total { get; set; }
    }
}