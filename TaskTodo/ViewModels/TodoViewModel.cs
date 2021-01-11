using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTodo.ApplicationCore.Entities;

namespace TaskTodo.ViewModels
{
    public class TodoViewModel
    {
        public IReadOnlyList<TodoItem> Items { get; set; }
    }
}
