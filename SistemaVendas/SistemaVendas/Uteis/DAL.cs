using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace SistemaVendas.Uteis
{
    // Data Access Layer
    public class DAL
    {
        private static string Server = "localhost";
        private static string Database = "sistema_abaco";
        private static string User = "root";
        private static string Password = "1234";
        private static string ConnectionString = $"Server={Server};Database={Database};Uid={User};Pwd={Password};Sslmode=none;Charset=utf8;";
        private static MySqlConnection Connection;

        //private static string Server = "den1.mysql6.gear.host";
        //private static string Database = "sistemaabaco";
        //private static string User = "sistemaabaco";
        //private static string Password = "Jv0d5ED4~?Ti";
        //private static string ConnectionString = $"Server={Server};Database={Database};Uid={User};Pwd={Password};Sslmode=none;Charset=utf8;";
        //private static MySqlConnection Connection;

        public DAL()
        {
            Connection = new MySqlConnection(ConnectionString);
            Connection.Open();
        }

        // Espera um parâmetro do tipo String
        // contendo um comando SQL do tipo SELECT
        public DataTable RetDataTable(string sql)
        {
            DataTable data = new DataTable();
            MySqlCommand Command = new MySqlCommand(sql, Connection);
            MySqlDataAdapter da = new MySqlDataAdapter(Command);
            da.Fill(data);
            return data;
        }

        public DataTable RetDataTable(MySqlCommand Command)
        {
            DataTable data = new DataTable();
            Command.Connection = Connection;            
            MySqlDataAdapter da = new MySqlDataAdapter(Command);
            da.Fill(data);
            return data;
        }

        // Espera um parâmetro do tipo String
        // contendo um comando SQL do tipo INSERT, UPDATE, DELETE
        public void ExecutarComandoSQL(string sql)
        {
            MySqlCommand Command = new MySqlCommand(sql, Connection);
            Command.ExecuteNonQuery();
        }

    }
}
