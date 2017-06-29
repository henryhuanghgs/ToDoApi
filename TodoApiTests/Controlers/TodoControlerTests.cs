using System;
using Xunit;
using TodoApi.Data;
using Microsoft.EntityFrameworkCore;
using TodoApi.Controllers;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using Microsoft.Extensions.Logging;
using Moq;

namespace TodoApiTests.Controlers
{
    public class TodoControlerTests
    {
        private TodoContext _context;
        ILogger<TodoController> _logger;

        public TodoControlerTests()
        {
            var builder = new DbContextOptionsBuilder<TodoContext>().UseInMemoryDatabase();
            _context = new TodoContext(builder.Options);

            var mock = new Mock<ILogger<TodoController>>();
			_logger = mock.Object;

            DbInitializer.Initialize(_context);
        }

        [Fact]
        public void TestGetAll()
        {
            var ctrl = new TodoController(_context, _logger);

            var items = ctrl.GetAll();
            var iter = items.GetEnumerator();

            iter.MoveNext();
            Assert.Equal(iter.Current.Name, "Item1");

			iter.MoveNext();
			Assert.Equal(iter.Current.Name, "Item2");
        }

        [Fact]
        public void TestGetById()
        {
            var ctrl = new TodoController(_context, _logger);

            var res = ctrl.GetById(1) as ObjectResult;
            Assert.NotNull(res);

            var item = res.Value as TodoItem;
            Assert.NotNull(item);

            Assert.Equal(item.Name, "Item1");
        }

        [Fact]
        public void TestSearch() 
        {
            var ctrl = new TodoController(_context, _logger);

            var res = ctrl.Search(name: "item");

            var items = res.ToArray();

            Assert.Equal(items.Count(), 2);

            Assert.Equal(items[0].Name, "Item1");
        }
    }
}
