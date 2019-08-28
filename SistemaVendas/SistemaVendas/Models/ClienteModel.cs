using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaVendas.Uteis;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace SistemaVendas.Models
{
    public class ClienteModel
    {
        public string Id { get; set; }
        public string Usuario_Id { get; set; }
        public string Empresa_Id { get; set; }

        [Required(ErrorMessage = "Informe o Nome do cliente")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe o CPF do cliente")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "Informe o Email do cliente")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "O e-mail informado é inválido!")]
        public string Email { get; set; }

        public string Senha { get; set; }
        public string Tipo { get; set; }
        public string Status { get; set; }
        public string Telefone { get; set; }

        public List<ClienteModel> ListarTodosClientes()
        {
            List<ClienteModel> lista = new List<ClienteModel>();
            ClienteModel item;
            DAL objDAL = new DAL();
            string sql = "select c.id, u.nome as usuario, e.nome_fantasia as empresa, u.cpf_cnpj as Cpf, u.email, u.senha, u.tipo, u.status " +
                         "from Cliente c inner join usuario u on u.id = c.id_usuario " +
                         "inner join empresa e on e.id = u.id_empresa order by u.nome asc";
            DataTable dt = objDAL.RetDataTable(sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new ClienteModel
                {
                    Id = dt.Rows[i]["Id"].ToString(),
                    Nome = dt.Rows[i]["Usuario"].ToString(),
                    Usuario_Id = dt.Rows[i]["Usuario"].ToString(),
                    Empresa_Id = dt.Rows[i]["Empresa"].ToString(),
                    CPF = dt.Rows[i]["Cpf"].ToString(),
                    Email = dt.Rows[i]["Email"].ToString(),
                    Senha = dt.Rows[i]["Senha"].ToString(),
                    Tipo = dt.Rows[i]["Tipo"].ToString(),
                    Status = dt.Rows[i]["Status"].ToString()
                };
                lista.Add(item);
            }

            return lista;
        }

        public ClienteModel RetornarCliente(int? id)
        {
            List<ClienteModel> lista = new List<ClienteModel>();
            ClienteModel item;
            DAL objDAL = new DAL();
            string sql = "select c.id, u.nome as usuario, e.nome_fantasia as empresa, u.cpf_cnpj as Cpf, u.email, u.senha, u.tipo, u.status " +
                         "from Cliente c inner join usuario u on u.id = c.id_usuario " +
                         $"inner join empresa e on e.id = u.id_empresa where c.id = '{id}' order by u.nome asc";
            DataTable dt = objDAL.RetDataTable(sql);
            var tipo = dt.Rows[0]["Tipo"].ToString();

            item = new ClienteModel
            {
                Id = dt.Rows[0]["Id"].ToString(),
                Nome = dt.Rows[0]["Usuario"].ToString(),
                Usuario_Id = dt.Rows[0]["Usuario"].ToString(),
                Empresa_Id = dt.Rows[0]["Empresa"].ToString(),
                CPF = dt.Rows[0]["Cpf"].ToString(),
                Email = dt.Rows[0]["Email"].ToString(),
                Senha = dt.Rows[0]["Senha"].ToString()
            };
            
            return item;
        }

        public List<EmpresaModel> RetornarListaEmpresas()
        {
            return new EmpresaModel().ListarTodasEmpresas();
        }

        public void Gravar()
        {
            DAL objDAL = new DAL();
            string sql = string.Empty;

            if(Id != null)
            {
                sql = "select u.id from cliente c inner join usuario u on c.id_usuario = u.id " +
                      $"where c.id = '{Id}'";
                DataTable dt = objDAL.RetDataTable(sql);
                string id_usuario = dt.Rows[0]["id"].ToString();

                sql = $"update usuario set id_empresa = '{Empresa_Id}', nome = '{Nome}', cpf_cnpj = '{CPF}', " +
                      $"email = '{Email}', tipo = '{Tipo}', status = '{EnumStatusUsuario.Ativo.GetHashCode()}' " +
                      $"where id = '{id_usuario}'";
                objDAL.ExecutarComandoSQL(sql);

                sql = $"UPDATE CLIENTE SET id_usuario = '{id_usuario}', telefone = '{Telefone}' WHERE id = '{Id}'";
            }
            else
            {
                sql = "insert into usuario (id_empresa, nome, cpf_cnpj, email, senha, tipo, status) " +
                      $"values ('{Empresa_Id}', '{Nome}', '{CPF}', '{Email}', '123456', '{EnumTipoUsuario.Cliente.GetHashCode()}', '{EnumStatusUsuario.Ativo.GetHashCode()}')";
                objDAL.ExecutarComandoSQL(sql);

                sql = $"select u.id from usuario u where u.cpf_cnpj = '{CPF}'";
                DataTable dt = objDAL.RetDataTable(sql);
                string id_usuario = dt.Rows[0]["id"].ToString();

                sql = $"insert into cliente(id_usuario, telefone) values('{id_usuario}', '{Telefone}')";
            }
            
            objDAL.ExecutarComandoSQL(sql);
        }

        public void Excluir(int id)
        {
            DAL objDAL = new DAL();
            string sql = $"DELETE FROM CLIENTE where id = '{id}'";
            objDAL.ExecutarComandoSQL(sql);
        }

    }
}
