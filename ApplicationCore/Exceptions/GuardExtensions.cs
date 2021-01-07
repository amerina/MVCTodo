using System;
using System.Collections.Generic;
using System.Text;
using TaskTodo.ApplicationCore.Entities;
using TaskTodo.ApplicationCore.Exceptions;

namespace Ardalis.GuardClauses
{
    public static class TodoItemGuards
    {
        public static void NullTodoItem(this IGuardClause guardClause, int id, TodoItem todoItem)
        {
            if (todoItem == null)
                throw new TodoItemNotFoundException(id);
        }

    }
}
