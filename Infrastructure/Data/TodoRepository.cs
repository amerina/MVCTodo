using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskTodo.ApplicationCore.Entities;
using TaskTodo.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    /// <summary>
    /// 实现仓储接口ITodoItemRepository
    /// </summary>
    public class TodoItemRepository : EfRepository<TodoItem>,ITodoItemRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        public TodoItemRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public Task<TodoItem> GetByIdWithTasksAsync(int id)
        {
            return _dbContext.Items.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
