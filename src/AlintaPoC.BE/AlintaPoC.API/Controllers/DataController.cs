using AlintaPoC.Application.Services;
using AlintaPoC.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlintaPoC.API.Controllers
{
    public class DataController : CommonController
    {
        private readonly ILogger<DataController> _logger;
        private readonly IAppService _appService;

        public DataController(
            ILogger<DataController> logger,
            IAppService appService)
        {
            _logger = logger;
            _appService = appService;
        }

        [HttpGet]
        public ActionResult GetAllPeople()
        {
            return Response(_appService.GetAllPeople());
        }

        [HttpGet("{id}")]
        public ActionResult GetPersonById(int id)
        {
            var vm = _appService.GetPersonById(id);

            if (vm == null)
            {
                return ResponseNotFound();
            }

            return Response(vm);
        }

        [HttpPost]
        public ActionResult AddPerson([FromBody] PersonDto value)
        {
            _appService.AddPerson(value);

            return Response("", value);
        }

        [HttpPut]
        public ActionResult UpdatePerson([FromBody] PersonDto value)
        {
            _appService.UpdatePerson(value);

            return Response();
        }

        [HttpDelete("{id}")]
        public ActionResult DeletePerson(int id)
        {
            _appService.DeletePerson(id);

            return Response();
        }

        [HttpGet]
        public async Task<ActionResult> GetAllTodo()
        {
            return Response(await _appService.GetAllTodoAsync());
        }

        [HttpGet("{partitionKey}/{rowKey}")]
        public async Task<ActionResult> GetTodoById(string partitionKey, string rowKey)
        {
            var vm = await _appService.GetTodoByIdAsync(partitionKey, rowKey);

            if (vm == null)
            {
                return ResponseNotFound();
            }

            return Response(vm);
        }

        [HttpPost]
        public async Task<ActionResult> AddTodoAsync([FromBody] TodoDto value)
        {
            await _appService.AddTodoAsync(value);

            return Response("", value);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateTodoAsync([FromBody] TodoDto value)
        {
            await _appService.UpdateTodoAsync(value);

            return Response();
        }

        [HttpDelete("{partitionKey}/{rowKey}")]
        public async Task<ActionResult> DeleteTodoAsync(string partitionKey, string rowKey)
        {
            await _appService.DeleteTodoAsync(partitionKey, rowKey);

            return Response();
        }
    }
}
