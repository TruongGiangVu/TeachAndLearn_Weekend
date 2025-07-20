using CuoiTuan3Api.Constants;
using CuoiTuan3Api.Dtos;
using CuoiTuan3Api.Dtos.Requests;
using CuoiTuan3Api.Models;
using CuoiTuan3Api.OracleDb;
using CuoiTuan3Api.Utils;
using Microsoft.AspNetCore.Components.Forms;

namespace CuoiTuan3Api.Services
{
    public class ToDoService
    {
        private readonly DatabaseConnect _database;

        public ToDoService(DatabaseConnect database)
        {
            _database = database;
        }

        /// <summary> Hàm truy vấn todo bằng model SearchToDoDto </summary>
        /// <param name="search">Model search đio</param>
        /// <returns>Trả về danh sách todo với response model dto</returns>
        public ResponseDto<List<ToDo>?> GetToDoWithSearchModel(SearchToDoDto search)
        {
            ResponseDto<List<ToDo>?> response = new ResponseDto<List<ToDo>?>();

            //----------------- bước 1----------------------
            // tiền xử lý 
            if (search.FromDate is null) // parameter
            {
                search.FromDate = DateTime.MinValue;
                //search.FromDate = DateTime.Now.AddDays(-90);
            }

            DateTime baThangTruoc = DateTime.Now.AddDays(-90); // variable
            if (search.FromDate is not null && search.FromDate < baThangTruoc)
            {
                response.ErrCode = ErrCode.DataWrong;
                response.ErrMessage = "From date trong 3 thang do lai";
                return response;
            }

            if (search.ToDate is null)
            {
                search.ToDate = DateTime.MaxValue;
            }

            // validate dữ liệu
            if (search.FromDate > search.ToDate)
            {
                response.ErrCode = ErrCode.DataWrong;
                response.ErrMessage = "From date phải nhỏ hơn To date";
                return response;
            }

            // tiền xử lý 
            // FromDate - 00:00:00
            // ToDate - 23:59:59
            if (search.FromDate is not null)
            {
                search.FromDate = Handler.SetToZeroTime(search.FromDate);
            }
            if (search.ToDate is not null)
            {
                search.ToDate = Handler.SetToFullTime(search.ToDate);
            }

            if (!string.IsNullOrEmpty(search.Status))
            {
                search.Status = search.Status.ToUpper().Trim();
                if (search.Status != ToDoStatus.DONE && search.Status != ToDoStatus.NONE)
                {
                    response.ErrCode = ErrCode.DataWrong;
                    response.ErrMessage = "Status phải là 'DONE' hoặc 'NONE'";
                    return response;
                }
            }


            // step 2 thực hiện
            List<ToDo>? todos = _database.GetToDo(search);

            // step 3 xử lý output
            response.ErrCode = ErrCode.Success;
            response.ErrMessage = "Thanh cong";
            response.Payload = todos;

            return response;
        }

        public ResponseDto<ToDo?> GetToDoById(int? id)
        {
            ResponseDto<ToDo?> response = new ResponseDto<ToDo?>();
            response.ErrCode = ErrCode.UnKnow;

            // step 1 validate dữ liệu
            if (id == null)
            {
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
            if (todo == null)
            {
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
