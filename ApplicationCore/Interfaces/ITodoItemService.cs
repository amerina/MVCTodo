using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskTodo.ApplicationCore.Entities;

namespace TaskTodo.ApplicationCore.Interfaces
{
    /// <summary>
    /// 接口有助于解耦你的那些类，也有助于测试
    /// </summary>
    public interface ITodoItemService
    {
        Task<IReadOnlyList<TodoItem>> GetIncompleteItemsAsync(string userId);

        Task<bool> AddItemAsync(string userId, TaskDetail task, bool isDone);

        Task MarkDoneAsync(int id, string userId);
    }
}
