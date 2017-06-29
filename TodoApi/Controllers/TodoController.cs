using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using TodoApi.Data;
using System.Linq;
using Microsoft.Extensions.Logging;
using Serilog;

namespace TodoApi.Controllers
{
	[Route("api/[controller]")]
	public class TodoController : Controller
	{
		private readonly TodoContext _context;
        private readonly ILogger<TodoController> _logger;

		public TodoController(TodoContext context, ILogger<TodoController> logger)
		{
            _context = context;
            _logger = logger;
		}

		[HttpGet]
		public IEnumerable<TodoItem> GetAll()
		{
            _logger.LogDebug("GetAll() is called.");

			return _context.TodoItems.ToList();
		}

		[HttpGet("{id}", Name = "GetTodo")]
		public IActionResult GetById(long id)
		{
            _logger.LogDebug("GetById() is called. id: {id}", id);

			var item = _context.TodoItems.FirstOrDefault(t => t.Id == id);
			if (item == null)
			{
				return NotFound();
			}

            var item2 = new { Id = item.Id, Name = item.Name };
            _logger.LogDebug("GetById(): one item is queried. {item}", item2);

			return new ObjectResult(item);
		}

		[HttpGet("search", Name = "Search")]
		public IEnumerable<TodoItem> Search([FromQuery]string name)
        {
            var items = _context.TodoItems.Where(i => i.Name.ToLower().Contains(name.ToLower()));

            return items.ToList();
		}

		[HttpPost]
		public IActionResult Create([FromBody] TodoItem item)
		{
			if (item == null)
			{
				return BadRequest();
			}

			_context.TodoItems.Add(item);
			_context.SaveChanges();

			return CreatedAtRoute("GetTodo", new { id = item.Id }, item);
		}

		[HttpPut("{id}")]
		public IActionResult Update(long id, [FromBody] TodoItem item)
		{
			if (item == null || item.Id != id)
			{
				return BadRequest();
			}

			var todo = _context.TodoItems.FirstOrDefault(t => t.Id == id);
			if (todo == null)
			{
				return NotFound();
			}

			todo.IsComplete = item.IsComplete;
			todo.Name = item.Name;

			_context.TodoItems.Update(todo);
			_context.SaveChanges();
			return new NoContentResult();
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(long id)
		{
			var todo = _context.TodoItems.First(t => t.Id == id);
			if (todo == null)
			{
				return NotFound();
			}

			_context.TodoItems.Remove(todo);
			_context.SaveChanges();
			return new NoContentResult();
		}
	}
}
