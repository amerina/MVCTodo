using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Text;
using TaskTodo.ApplicationCore.Entities;

namespace TaskTodo.ApplicationCore.Specifications
{
    public class ItemSpecification : Specification<TodoItem>
    {
        public ItemSpecification(int id)
        {
            Query
              .Where(b => b.Id == id);
        }
    }
}
