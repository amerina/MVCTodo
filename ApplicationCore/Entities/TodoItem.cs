using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using System.Text;
using TaskTodo.ApplicationCore.Interfaces;

namespace TaskTodo.ApplicationCore.Entities
{
    /// <summary>
    /// 一个TodoItem包含一个需要完成的Task
    /// 
    /// </summary>
    public class TodoItem : BaseEntity, IAggregateRoot
    {
        private TodoItem()
        {
            // required by EF
        }

        public TodoItem(string userId, TaskDetail task, bool isDone)
        {
            Guard.Against.NullOrEmpty(userId, nameof(userId));
            Guard.Against.Null(task, nameof(task));

            UserId = userId;
            Task = task;
            IsDone = isDone;
        }

        public bool IsDone { get; private set; }
        public TaskDetail Task { get; private set; }
        public DateTimeOffset? DueAt { get; private set; } = DateTimeOffset.Now;

        public string UserId { get; private set; }

        public void MarkDone(bool isDone)
        {
            IsDone = isDone;
        }

        public void UpdateTodoItem(TaskDetail task, bool isDone)
        {
            Guard.Against.Null(task, nameof(task));
         
            Task = task;
            IsDone = isDone;
        }
    }
}
