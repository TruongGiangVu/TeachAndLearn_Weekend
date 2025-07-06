using CuoiTuan1Api.Models;
using CuoiTuan1Api.Utils;
using Oracle.ManagedDataAccess.Client;
using System.Globalization;

namespace CuoiTuan1Api.OracleDb
{
    // Dependency injection DI 
    public class DatabaseConnect // service database 
    {
        public string connectionString = string.Empty;
        public DatabaseConnect(IConfiguration configuration)
        {
            connectionString = configuration["AppSettings:ConnectionString"];
        }
        
        
        public List<ToDo> GetToDo()
        {
            TryParse tryParse = new TryParse();
            List<ToDo> data = [];
            using OracleConnection con = new OracleConnection(connectionString);
            con.Open();
            string sql = "select id, name ,status, is_done, start_date from ct1_todo";
            using OracleCommand cmd = new OracleCommand(sql, con);
            var reader = cmd.ExecuteReader();
            while (reader.Read()) { 
                ToDo toDo = new ToDo();
                toDo.Id = int.Parse(reader["id"].ToString());
                toDo.Name = reader["name"].ToString();
                toDo.IsDone = reader["is_done"].ToString() == "Y" ? true: false; // Y/ N 
                toDo.Status = reader["status"].ToString();
                toDo.StartDate = TryParse.DateParse(reader["start_date"].ToString()) ?? DateTime.Now;

                data.Add(toDo);
            }
            return data;
        }

        public ToDo? GetToDoId(int id)
        {
            ToDo? data = null;
            using OracleConnection con = new OracleConnection(connectionString);
            con.Open();
            string sql = $"select id, name ,status, is_done, to_char(start_date, 'dd/MM/yyyy') start_date from ct1_todo where id={id}";
            using OracleCommand cmd = new OracleCommand(sql, con);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                data = new ToDo();
                data.Id = int.Parse(reader["id"].ToString());
                data.Name = reader["name"].ToString();
                data.IsDone = reader["is_done"].ToString() == "Y" ? true : false; // Y/ N 
                data.Status = reader["status"].ToString();
                data.StartDate = DateTime.ParseExact(reader["start_date"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            return data;
        }
    }
}
