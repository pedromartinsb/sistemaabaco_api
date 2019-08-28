using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVendas.Models
{
    public class ItemProdutoProjeto
    {
        public string CodigoProduto { get; set; }
        public string QtdeProduto { get; set; }
        public string PrecoUnitario { get; set; }
        public string Total { get; set; }
    }
}
