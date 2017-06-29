﻿using System;
using TodoApi.Data;
using TodoApi.Models;
using System.Linq;

namespace TodoApi.Data
{
    public static class DbInitializer
    {
        public static void Initialize(TodoContext context) 
        {
            if (context.TodoItems.Any()) 
                return;

            var items = new TodoItem[]
            {
                new TodoItem { Name = "Item1" },
                new TodoItem { Name = "Item2" }
            };

            foreach (var i in items) 
            {
				context.TodoItems.Add(i);   
            }

			context.SaveChanges();

        }
    }
}
