using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskTodo.ApplicationCore.Entities;

namespace TaskTodo.ApplicationCore.Interfaces
{
    /// <summary>
    /// 定义特殊的仓库方法
    /// </summary>
    public interface ITodoItemRepository : IAsyncRepository<TodoItem>
    {
        Task<TodoItem> GetByIdWithTasksAsync(int id);
    }
}
