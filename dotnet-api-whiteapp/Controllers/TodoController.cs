using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using dotnet_api_whiteapp.Models;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace dotnet_api_whiteapp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class TodoController : ControllerBase
    {
        private readonly ILogger<TodoController> _logger;

        public TodoController(ILogger<TodoController> logger)
        {
            _logger = logger;
        }

        [HttpGet("GetAllTodoItems")]
        public ActionResult<IEnumerable<WeatherForecast>> Get()
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

        [HttpGet("GetTodoItem")]
        public ActionResult<WeatherForecast> Get(int id)
        {
            var rng = new Random();
            var todoItem = new ToDoItem
            {
                Id = id,
                Date = DateTime.Now.AddDays(rng.Next(0, 20)),
                Title = $"{rng.Next(-20, 55)} Task"
            };

            return Ok(todoItem);
        }

        [HttpPost("CreateTodoItem")]
        public ActionResult<ToDoItem> Post(DateTime date, string title)
        {
            var rng = new Random();
            var todoItem = new ToDoItem { Id = rng.Next(0, 10), Date = date, Title = title };
            return Ok(todoItem);
        }
    }
}
