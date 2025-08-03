using CuoiTuan3Api.Constants;
using CuoiTuan3Api.Dtos;
using CuoiTuan3Api.Dtos.Requests;
using CuoiTuan3Api.Models;
using CuoiTuan3Api.OracleDb;
using CuoiTuan3Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Configuration;

namespace CuoiTuan3Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly Serilog.ILogger _log = Log.ForContext<HomeController>();
        private readonly IDatabaseConnect _database;
        private readonly IToDoService _service;
        private readonly ILogger<HomeController> _logger;
        public HomeController(IDatabaseConnect database, IToDoService service, 
                ILogger<HomeController> logger)
        {
            _database = database;
            _service = service;
            _logger = logger;
        }
        [HttpGet]
        public IActionResult IndexHome()
        {
            Parent<ChildA> parentA = new Parent<ChildA>();
            parentA.Data = new ChildA();

            Parent<ChildB> parentB = new Parent<ChildB>();
            parentB.Data = new ChildB();


            _log.Information("Log bằng readonly");
            _logger.LogInformation("Log bằng dependency inject");
            Log.Information("Log bằng static");
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
