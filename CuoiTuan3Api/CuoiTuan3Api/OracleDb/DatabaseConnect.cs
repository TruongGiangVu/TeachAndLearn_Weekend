using CuoiTuan3Api.Dtos.Requests;
using CuoiTuan3Api.Models;
using CuoiTuan3Api.Utils;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Data;
using System.Globalization;

namespace CuoiTuan3Api.OracleDb
{
    // Dependency injection DI 
    public class DatabaseConnect // service database 
    {
        public string connectionString = string.Empty;
        public DatabaseConnect(IConfiguration configuration)
        {
            connectionString = configuration["AppSettings:ConnectionString"];
        }


        //        public List<ToDo> GetToDo(SearchToDoDto search)
        //        {
        //            TryParse tryParse = new TryParse();
        //            List<ToDo> data = [];
        //            using OracleConnection con = new OracleConnection(connectionString);
        //            con.Open();
        //            string sql = @$"
        //select id, name ,status, is_done, start_date from ct1_todo
        //where 
        //";
        //            using OracleCommand cmd = new OracleCommand(sql, con);
        //            var reader = cmd.ExecuteReader();
        //            while (reader.Read())
        //            {
        //                ToDo toDo = new ToDo();
        //                toDo.Id = int.Parse(reader["id"].ToString());
        //                toDo.Name = reader["name"].ToString();
        //                toDo.IsDone = TryParse.Boolean(reader["is_done"]); // Y/ N 
        //                toDo.Status = reader["status"].ToString();
        //                toDo.StartDate = TryParse.DateParse(reader["start_date"].ToString()) ?? DateTime.Now;

        //                data.Add(toDo);
        //            }
        //            return data;
        //        }

        public List<ToDo> GetToDo(SearchToDoDto search)
        {
            List<ToDo> data = [];
            try
            {
                using OracleConnection con = new OracleConnection(connectionString);
                con.Open();
                using OracleCommand cmd = new OracleCommand("PKG_CT.GET_TODO", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("p_name", OracleDbType.Varchar2).Value = search.Name;
                cmd.Parameters.Add("p_status", OracleDbType.Varchar2).Value = search.Status;
                cmd.Parameters.Add("p_from_date", OracleDbType.Date).Value = search.FromDate;
                cmd.Parameters.Add("p_to_date", OracleDbType.Date).Value = search.ToDate;
                cmd.Parameters.Add("o_ref_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();

                OracleDataReader reader = ((OracleRefCursor)cmd.Parameters["o_ref_cursor"].Value).GetDataReader();
                data = [];
                if (reader.HasRows)
                    while (reader.Read())
                    {
                        ToDo toDo = new ToDo();
                        toDo.Id = int.Parse(reader["id"].ToString());
                        toDo.Name = reader["name"].ToString();
                        toDo.IsDone = TryParse.Boolean(reader["is_done"]); // Y/ N 
                        toDo.Status = reader["status"].ToString();
                        toDo.StartDate = TryParse.DateParse(reader["start_date"].ToString()) ?? DateTime.Now;

                        data.Add(toDo);
                    }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetToDo error: {ex}");
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
                data.IsDone = TryParse.Boolean(reader["is_done"]); // Y/ N 
                data.Status = reader["status"].ToString();
                data.StartDate = DateTime.ParseExact(reader["start_date"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            return data;
        }
    }
}
