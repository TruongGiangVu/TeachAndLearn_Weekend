using CuoiTuan2Api.Constants;
using CuoiTuan2Api.Dtos;
using CuoiTuan2Api.Dtos.Requests;
using CuoiTuan2Api.Models;
using CuoiTuan2Api.OracleDb;
using CuoiTuan2Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;

namespace CuoiTuan2Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly DatabaseConnect _database;
        private readonly ToDoService _service;

        public HomeController(DatabaseConnect database, ToDoService service)
        {
            _database = database;
            _service = service;
        }
        [HttpGet]
        public IActionResult Index()
        {
            string abc = ErrCode.Success;
            //ResponseModel<ToDo> responseModel = new ResponseModel<ToDo>();
            //responseModel.Payload.Name
            return Ok("service is running " + DateTime.Now.ToString());
        }

        [HttpGet("todo")]
        [ProducesResponseType(typeof(ResponseDto<List<ToDo>?>), StatusCodes.Status200OK)]
        public IActionResult GetToDo([FromQuery] SearchToDoDto searchInput)
        {
            var responseModel = new ResponseDto<List<ToDo>>();
            responseModel.Payload = _database.GetToDo(searchInput);
            responseModel.ErrCode = ErrCode.Success;
            responseModel.ErrMessage = "Thanhf cong";
            return Ok(responseModel);
        }

        [HttpGet("todo2")]
        [ProducesResponseType(typeof(ResponseDto<List<ToDo>?>), StatusCodes.Status200OK)]
        public IActionResult GetToDoWithSearch([FromQuery] SearchToDoDto searchInput)
        {
            //if (!ModelState.IsValid) {
            //    Console.WriteLine(ModelState)
            //    return Ok("fdsfdsf");
            //}
            ResponseDto<List<ToDo>?> response = _service.GetToDoWithSearchModel(searchInput);
            return Ok(response);
        }

        [HttpGet("todo/{id}")]
        [ProducesResponseType(typeof(ResponseDto<List<ToDo>?>), StatusCodes.Status200OK)]
        public IActionResult GetToDoById(int? id)
        {
            ResponseDto<ToDo?> response = _service.GetToDoById(id);
            return Ok(response);
        }
    }
}
