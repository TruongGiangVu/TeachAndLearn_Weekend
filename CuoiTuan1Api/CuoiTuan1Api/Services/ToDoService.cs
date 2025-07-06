using CuoiTuan1Api.Constants;
using CuoiTuan1Api.Dtos;
using CuoiTuan1Api.Models;
using CuoiTuan1Api.OracleDb;

namespace CuoiTuan1Api.Services
{
    public class ToDoService
    {
        private readonly DatabaseConnect _database;

        public ToDoService(DatabaseConnect database)
        {
            _database = database;
        }

        public ResponseModel<ToDo?> GetToDoById(int? id) {
            ResponseModel<ToDo?> response = new ResponseModel<ToDo?>();
            response.ErrCode = ErrCode.UnKnow;

            // step 1 validate dữ liệu
            if (id == null) {
                response.ErrCode = ErrCode.Required;
                response.ErrMessage = "Truyen thieu id nha";
                return response;
            }

            if (id <= 0)
            {
                response.ErrCode = ErrCode.DataWrong;
                response.ErrMessage = " id phai lon hon 0";
                return response;
            }

            // step 2 thực hiện
            ToDo? todo = _database.GetToDoId(id.Value);

            // step 3 xử lý output
            if (todo == null) {
                response.ErrCode = ErrCode.NotFound;
                response.ErrMessage = "ToDo id nay ko ton tai";
                return response;
            }

            response.ErrCode = ErrCode.Success;
            response.ErrMessage = "Thanh cong";
            response.Payload = todo;

            return response;
        }
    }
}
