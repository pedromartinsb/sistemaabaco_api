using Newtonsoft.Json;
using SistemaVendas.Uteis;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVendas.Models
{
    public class ProjetoModel
    {
        public string Id { get; set; }
        public string Cliente_Id { get; set; }
        public string Empresa_Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Etapa_Id { get; set; }
        public double Total { get; set; }
        public int Data { get; set; }
        public string ListaProdutos { get; set; }
        public string ListaColaboradores { get; set; }

        public List<ProjetoModel> ListarTodosProjetos()
        {
            List<ProjetoModel> lista = new List<ProjetoModel>();
            ProjetoModel item;
            DAL objDAL = new DAL();
            string sql = "select p.id, u.nome as usuario, p.nome, p.descricao, p.total, p.data, e.nome as etapa from projeto p " +
                         "inner join etapa e on e.id = p.id_etapa " +
                         "inner join cliente c on c.id = p.id_cliente " +
                         "inner join usuario u on u.id = c.id_usuario " +
                         "order by p.nome asc";
            DataTable dt = objDAL.RetDataTable(sql);
            var dataProjeto = DateTime.Parse(dt.Rows[0]["data"].ToString());

            DateTime dataHoje = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            var diferencaDatas = (int)dataHoje.Subtract(dataProjeto).TotalDays;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new ProjetoModel
                {
                    Id = dt.Rows[i]["Id"].ToString(),
                    Cliente_Id = dt.Rows[i]["Usuario"].ToString(),
                    Nome = dt.Rows[i]["Nome"].ToString(),
                    Descricao = dt.Rows[i]["Descricao"].ToString(),
                    Etapa_Id = dt.Rows[i]["Etapa"].ToString(),
                    Data = diferencaDatas,
                    Total = double.Parse(dt.Rows[i]["total"].ToString())
                };
                lista.Add(item);
            }

            return lista;
        }

        public ProjetoModel RetornarProjeto(int? id)
        {
            ProjetoModel item;
            DAL objDAL = new DAL();
            string sql = "select p.id, u.nome as usuario, p.nome, p.descricao, p.total, p.data, e.nome as etapa from projeto p " +
                         "inner join etapa e on e.id = p.id_etapa " +
                         "inner join cliente c on c.id = p.id_cliente " +
                         "inner join usuario u on u.id = c.id_usuario " +
                         $"where p.id = '{id}'" +
                         "order by p.nome asc";
            DataTable dt = objDAL.RetDataTable(sql);
            var dataProjeto = DateTime.Parse(dt.Rows[0]["data"].ToString());

            DateTime dataHoje = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            var diferencaDatas = (int)dataHoje.Subtract(dataProjeto).TotalDays;

            item = new ProjetoModel
            {
                Id = dt.Rows[0]["Id"].ToString(),
                Cliente_Id = dt.Rows[0]["Usuario"].ToString(),
                Nome = dt.Rows[0]["Nome"].ToString(),
                Descricao = dt.Rows[0]["Descricao"].ToString(),
                Etapa_Id = dt.Rows[0]["Etapa"].ToString(),
                Data = diferencaDatas,
                //Data = DateTime.Parse(dt.Rows[0]["data"].ToString()).ToString("dd/MM/yyyy"),
                Total = double.Parse(dt.Rows[0]["total"].ToString())
            };

            return item;
        }

        public List<ProjetoModel> RetornarProjetosPorEtapa(int id)
        {
            if(id == 0)
            {
                List<ProjetoModel> listaVazia = new List<ProjetoModel>();
                return listaVazia;
            }

            List<ProjetoModel> lista = new List<ProjetoModel>();
            ProjetoModel item;
            DAL objDAL = new DAL();
            string sql = "select p.id, u.nome as usuario, p.nome, p.descricao, p.total, p.data, e.nome as etapa from projeto p " +
                         "inner join etapa e on e.id = p.id_etapa " +
                         "inner join cliente c on c.id = p.id_cliente " +
                         "inner join usuario u on u.id = c.id_usuario " +
                         $"where e.id = '{id}'" +
                         "order by p.nome asc";
            DataTable dt = objDAL.RetDataTable(sql);

            if(dt.Rows.Count > 0)
            {
                var dataProjeto = DateTime.Parse(dt.Rows[0]["data"].ToString());

                DateTime dataHoje = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

                var diferencaDatas = (int)dataHoje.Subtract(dataProjeto).TotalDays;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    item = new ProjetoModel
                    {
                        Id = dt.Rows[i]["Id"].ToString(),
                        Cliente_Id = dt.Rows[i]["Usuario"].ToString(),
                        Nome = dt.Rows[i]["Nome"].ToString(),
                        Descricao = dt.Rows[i]["Descricao"].ToString(),
                        Etapa_Id = dt.Rows[i]["Etapa"].ToString(),
                        Data = diferencaDatas,
                        Total = double.Parse(dt.Rows[i]["total"].ToString())
                    };
                    lista.Add(item);
                }
            }
            else
            {
                List<ProjetoModel> listaVazia = new List<ProjetoModel>();
                return listaVazia;
            }            

            return lista;
        }

        public List<ClienteModel> RetornarListaClientes()
        {
            return new ClienteModel().ListarTodosClientes();
        }

        public List<ColaboradorModel> RetornarListaColaboradores()
        {
            return new ColaboradorModel().ListarTodosColaboradores();
        }

        public List<ProdutoModel> RetornarListaProdutos()
        {
            return new ProdutoModel().ListarTodosProdutos();
        }

        public List<EmpresaModel> RetornarListaEmpresas()
        {
            return new EmpresaModel().ListarTodasEmpresas();
        }

        public void Inserir()
        {
            DAL objDal = new DAL();

            string dataProjeto = DateTime.Now.Date.ToString("yyyy/MM/dd");

            string sql = "insert into projeto (id_cliente, id_etapa, id_empresa, nome, descricao, data, total) " +
                $"values ({Cliente_Id}, {Etapa_Id}, {Empresa_Id}, '{Nome}', '{Descricao}', '{dataProjeto}', {Total.ToString().Replace(",", ".")})";
            objDal.ExecutarComandoSQL(sql);

            // Recuperar o ID do Projeto
            sql = $"select id from projeto where data='{dataProjeto}' " +
                $"and id_cliente = '{Cliente_Id}' order by id desc limit 1";
            DataTable dt = objDal.RetDataTable(sql);
            string id_projeto = dt.Rows[0]["id"].ToString();


            if(ListaProdutos != null)
            {
                // Serializar o JSON da lista de produtos selecionados e gravá-los na tabela itens_projeto
                List<ItemProdutoProjeto> lista_produtos = JsonConvert.DeserializeObject<List<ItemProdutoProjeto>>(ListaProdutos);
                for (int i = 0; i < lista_produtos.Count; i++)
                {
                    sql = "insert into itens_projeto (id_projeto, id_produto, qtde_produto, preco_produto) " +
                        $"values ({id_projeto}, {lista_produtos[i].CodigoProduto.ToString()}, " +
                        $"{lista_produtos[i].QtdeProduto.ToString()}, " +
                        $"{lista_produtos[i].PrecoUnitario.ToString().Replace(",", ".")})";
                    objDal.ExecutarComandoSQL(sql);

                    // Baixa o produto do Estoque
                    //sql = " update produto " +
                    //      " set quantidade_estoque = (quantidade_estoque - " + int.Parse(lista_produtos[i].QtdeProduto.ToString()) + ") " +
                    //      $" where id = {lista_produtos[i].CodigoProduto.ToString()} ";
                    //objDal.ExecutarComandoSQL(sql);
                }
            }

            
            if(ListaColaboradores != null)
            {
                List<ColaboradorProjetoModel> lstColaboradorProjeto = JsonConvert.DeserializeObject<List<ColaboradorProjetoModel>>(ListaColaboradores);
                for (int i = 0; i < lstColaboradorProjeto.Count; i++)
                {
                    sql = "insert into colaborador_projeto (id_projeto, id_colaborador)" +
                          $"values ({id_projeto},{lstColaboradorProjeto[i].CodigoColaborador.ToString()})";
                    objDal.ExecutarComandoSQL(sql);
                }
            }
            
        }
    }
}
