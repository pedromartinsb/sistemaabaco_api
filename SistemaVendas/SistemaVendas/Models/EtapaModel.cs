using SistemaVendas.Uteis;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVendas.Models
{
    public class EtapaModel
    {
        public string Id { get; set; }
        public string Nome { get; set; }

        public List<EtapaModel> ListarTodasEtapas()
        {
            List<EtapaModel> lista = new List<EtapaModel>();
            EtapaModel item;
            DAL objDal = new DAL();
            string sql = "select id, nome from etapa order by id asc";
            DataTable dataTable = objDal.RetDataTable(sql);

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                item = new EtapaModel
                {
                    Id = dataTable.Rows[i]["id"].ToString(),
                    Nome = dataTable.Rows[i]["nome"].ToString()
                };
                lista.Add(item);
            }

            return lista;
        }

        public EtapaModel RetornarEtapa(int? id)
        {
            List<EtapaModel> lista = new List<EtapaModel>();
            EtapaModel etapa;
            DAL objDal = new DAL();
            string sql = $"select id, nome from etapa where id = '{id}'";
            DataTable dataTable = objDal.RetDataTable(sql);

            etapa = new EtapaModel
            {
                Id = dataTable.Rows[0]["id"].ToString(),
                Nome = dataTable.Rows[0]["nome"].ToString()
            };

            return etapa;
        }

        public void Gravar()
        {
            DAL objDal = new DAL();
            string sql = string.Empty;

            if(Id != null)
            {
                sql = $"update etapa set nome = '{Nome}' where id = '{Id}'";
            }
            else
            {
                sql = $"insert into etapa(nome) values('{Nome}')";
            }

            objDal.ExecutarComandoSQL(sql);
        }

        public void Excluir(int id)
        {
            DAL objDal = new DAL();
            string sql = $"delete from etapa where id = '{id}'";
            objDal.ExecutarComandoSQL(sql);
        }
    }
}
