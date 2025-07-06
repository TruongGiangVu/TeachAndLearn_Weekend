using CuoiTuan1Api.Constants;
using CuoiTuan1Api.Dtos;
using CuoiTuan1Api.Models;
using CuoiTuan1Api.OracleDb;
using CuoiTuan1Api.Services;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;

namespace CuoiTuan1Api.Controllers
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
        [ProducesResponseType(typeof(ResponseModel<List<ToDo>?>), StatusCodes.Status200OK)]
        public IActionResult GetToDo()
        {
            var responseModel = new ResponseModel<List<ToDo>>();
            responseModel.Payload = _database.GetToDo();
            responseModel.ErrCode = ErrCode.Success;
            responseModel.ErrMessage = "Thanhf cong";
            return Ok(responseModel);
        }

        [HttpGet("todo/{id}")]
        [ProducesResponseType(typeof(ResponseModel<List<ToDo>?>), StatusCodes.Status200OK)]
        public IActionResult GetToDoById(int? id)
        {
            ResponseModel<ToDo?> response = _service.GetToDoById(id);
            return Ok(response);
        }
    }
}
