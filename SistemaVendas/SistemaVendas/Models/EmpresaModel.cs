using SistemaVendas.Uteis;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVendas.Models
{
    public class EmpresaModel
    {
        public string Id { get; set; }
        public string Nome_Fantasia { get; set; }
        public string Razao_Social { get; set; }
        public string Cnpj { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }

        public List<EmpresaModel> ListarTodasEmpresas()
        {
            List<EmpresaModel> lista = new List<EmpresaModel>();
            EmpresaModel item;
            DAL objDal = new DAL();
            string sql = "select id, nome_fantasia, razao_social, cnpj, telefone, endereco from empresa order by nome_fantasia asc";
            DataTable dataTable = objDal.RetDataTable(sql);

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                item = new EmpresaModel
                {
                    Id = dataTable.Rows[i]["id"].ToString(),
                    Nome_Fantasia = dataTable.Rows[i]["nome_fantasia"].ToString(),
                    Razao_Social = dataTable.Rows[i]["razao_social"].ToString(),
                    Cnpj = dataTable.Rows[i]["cnpj"].ToString(),
                    Telefone = dataTable.Rows[i]["telefone"].ToString(),
                    Endereco = dataTable.Rows[i]["endereco"].ToString()
                };
                lista.Add(item);
            }

            return lista;
        }

        public EmpresaModel RetornarEmpresa(int? id)
        {
            List<EmpresaModel> lista = new List<EmpresaModel>();
            EmpresaModel empresa;
            DAL objDal = new DAL();
            string sql = $"select id, nome_fantasia, razao_social, cnpj, telefone, endereco from empresa where id = {id}";
            DataTable dataTable = objDal.RetDataTable(sql);
            empresa = new EmpresaModel
            {
                Id = dataTable.Rows[0]["id"].ToString(),
                Nome_Fantasia = dataTable.Rows[0]["nome_fantasia"].ToString(),
                Razao_Social = dataTable.Rows[0]["razao_social"].ToString(),
                Cnpj = dataTable.Rows[0]["cnpj"].ToString(),
                Telefone = dataTable.Rows[0]["telefone"].ToString(),
                Endereco = dataTable.Rows[0]["endereco"].ToString()
            };

            return empresa;
        }

        public void Gravar()
        {
            DAL objDal = new DAL();
            string sql = string.Empty;

            if(Id != null)
            {
                sql = $"update empresa set nome_fantasia = '{Nome_Fantasia}', razao_social = '{Razao_Social}', " +
                      $"cnpj = '{Cnpj}', telefone = '{Telefone}', endereco = '{Endereco}' " +
                      $"where id = '{Id}'";
            }
            else
            {
                sql = "insert into empresa(nome_fantasia, razao_social, cnpj, telefone, endereco) " +
                      $"values('{Nome_Fantasia}', '{Razao_Social}', '{Cnpj}', '{Telefone}', '{Endereco}')";
            }

            objDal.ExecutarComandoSQL(sql);
        }

        public void Excluir(int id)
        {
            DAL objDal = new DAL();
            string sql = $"delete from empresa where id = '{id}'";
            objDal.ExecutarComandoSQL(sql);
        }
    }
}
