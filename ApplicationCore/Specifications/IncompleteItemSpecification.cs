using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Text;
using TaskTodo.ApplicationCore.Entities;

namespace TaskTodo.ApplicationCore.Specifications
{
    public class IncompleteItemSpecification:Specification<TodoItem>
    {
        public IncompleteItemSpecification(string userId)
        {
            Query
                .Where(b => b.UserId == userId)
                .Where(b => !b.IsDone);

        }
    }
}
