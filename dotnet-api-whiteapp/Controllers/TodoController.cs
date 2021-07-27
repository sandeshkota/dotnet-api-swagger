using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using dotnet_api_swagger.Models;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using dotnet_api_swagger.Contracts;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Authorization;

namespace dotnet_api_swagger.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [SwaggerTag("Create, read, update and delete Todo Items")]
    public class TodoController : ControllerBase
    {
        private readonly ILogger<TodoController> _logger;

        public TodoController(ILogger<TodoController> logger)
        {
            _logger = logger;
        }

        [HttpGet("GetAllTodoItems")]
        [SwaggerOperation(Summary ="Gets All Todo Items", Description = "Returns All ToDo Items that are created", Tags = new string[] { "Todo" })]
        [SwaggerResponse(StatusCodes.Status200OK, "Loaded successfully", typeof(IEnumerable<ToDoItem>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request", typeof(ErrorContract))]
        public ActionResult<IEnumerable<ToDoItem>> Get()
        {
            var rng = new Random();
            var todoItems = Enumerable.Range(1, 5).Select(index => new ToDoItem
            {
                Date = DateTime.Now.AddDays(index),
                Id = rng.Next(0, 20),
                Title = $"{rng.Next(-20, 55)} Task"
            })
            .ToArray();

            return Ok(todoItems);
        }

        /// <summary>
        ///     Retrieves a specific todo item by unique id
        /// </summary>
        /// <remarks>Awesomeness!</remarks>
        /// <param name="id" example="123">The Todo Item id</param>
        /// <response code="200">Todo Item returned successfully</response>
        /// <response code="400">Todo item has missing/invalid values</response>
        /// <response code="500">Oops! Can't get todo item right now</response>
        [HttpGet("{id}", Name = "GetTodoItem")]
        [SwaggerOperation(Summary = "Gets All Todo Items", Description = "Load Todo Items by Id", Tags = new string[] { "Todo" })]
        [ProducesResponseType(typeof(ToDoItem), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorContract), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(500)]
        public ActionResult<ToDoItem> Get(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var rng = new Random();
            var todoItem = new ToDoItem
            {
                Id = id,
                Date = DateTime.Now.AddDays(rng.Next(0, 20)),
                Title = $"{rng.Next(-20, 55)} Task"
            };

            return Ok(todoItem);
        }

        /// <summary>
        ///     Creates a todo item
        /// </summary>
        /// <param name="date" example="2021-07-27">The Todo Item Date</param>
        /// <param name="title" example="My Todo Item">The Todo Item Title</param>
        //[Authorize]
        [HttpPost("CreateTodoItem")]
        [ApiExplorerSettings(GroupName = "v1")]
        [SwaggerOperation(Summary = "Saves Todo Item", Description = "Create a Todo Item", Tags = new string[] { "Todo" })]
        [SwaggerResponse(StatusCodes.Status200OK, "Saved successfully", typeof(ToDoItem))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request", typeof(ErrorContract))]
        public ActionResult<ToDoItem> Post([FromForm]DateTime date, [FromForm, BindRequired] string title)
        {
            var rng = new Random();
            var todoItem = new ToDoItem { Id = rng.Next(0, 10), Date = date, Title = title };
            return Ok(todoItem);
        }


        //[Authorize]
        [HttpPost("CreateTodoItemV2")]
        [ApiExplorerSettings(GroupName = "v2")]
        [SwaggerOperation(
            Summary = "Saves Todo Item", 
            Description = "Create a Todo Item",
            OperationId = "CreateTodoItem",
            Tags = new string[] { "Todo", "Create" })]
        [SwaggerResponse(StatusCodes.Status200OK, "Saved successfully", typeof(ToDoItem))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request", typeof(ErrorContract))]
        public ActionResult<ToDoItem> Post([FromBody]ToDoItem todoItem)
        {
            return Ok(todoItem);
        }


        [Obsolete]
        [HttpDelete("{id}", Name = "DeleteTodoItem")]
        [SwaggerOperation(Summary = "Deletes Todo Item", Description = "Delete a Todo Item", Tags = new string[] { "Todo", "Obsolete" })]
        //[Authorize]
        public IActionResult Delete(int id)
        {
            return Ok();
        }

        [HttpPut]
        [ApiExplorerSettings(IgnoreApi = true)]
        [SwaggerOperation(Summary = "Saves Todo Item", Description = "Create a Todo Item", Tags = new string[] { "Todo" })]
        //[Authorize]
        public IActionResult Put(ToDoItem todoItem)
        {
            return Ok();
        }
    }
}
