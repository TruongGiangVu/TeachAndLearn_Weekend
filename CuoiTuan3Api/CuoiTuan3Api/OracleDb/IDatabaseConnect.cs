using CuoiTuan3Api.Dtos.Requests;
using CuoiTuan3Api.Models;

namespace CuoiTuan3Api.OracleDb
{
    public interface IDatabaseConnect
    {
        List<ToDo> GetToDo(SearchToDoDto search);
        ToDo? GetToDoId(int id);
    }
}
