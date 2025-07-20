using CuoiTuan3Api.Dtos;
using CuoiTuan3Api.Dtos.Requests;
using CuoiTuan3Api.Models;

namespace CuoiTuan3Api.Services
{
    public interface IToDoService
    {
        ResponseDto<List<ToDo>?> GetToDoWithSearchModel(SearchToDoDto search);
        ResponseDto<ToDo?> GetToDoById(int? id);
    }
}
