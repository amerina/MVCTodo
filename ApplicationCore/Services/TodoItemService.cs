using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskTodo.ApplicationCore.Entities;
using TaskTodo.ApplicationCore.Interfaces;
using TaskTodo.ApplicationCore.Specifications;

namespace TaskTodo.ApplicationCore.Services
{
    public class TodoItemService : ITodoItemService
    {
        private readonly IAsyncRepository<TodoItem> _todoItemRepository;
        public TodoItemService(IAsyncRepository<TodoItem> todoItemRepository)
        {
            _todoItemRepository = todoItemRepository;
        }
        public async Task<bool> AddItemAsync(string userId, TaskDetail task, bool isDone)
        {
            var newItem = new TodoItem(userId, task, isDone);

            await _todoItemRepository.AddAsync(newItem);

            return newItem.Id > 0;
        }

        public async Task<IReadOnlyList<TodoItem>> GetIncompleteItemsAsync(string userId)
        {
            //使用守卫组件简化验证
            Guard.Against.NullOrWhiteSpace(userId, "UserId");

            //使用规约模式简化查询条件
            var itemsSpec = new IncompleteItemSpecification(userId);

            return await _todoItemRepository.ListAsync(itemsSpec);
        }

        public async Task MarkDoneAsync(int id)
        {
            var itemsSpec = new ItemSpecification(id);
            var item = await _todoItemRepository.FirstOrDefaultAsync(itemsSpec);

            Guard.Against.NullTodoItem(id, item);

            item.MarkDone(true);

            await _todoItemRepository.UpdateAsync(item);
        }
    }
}
