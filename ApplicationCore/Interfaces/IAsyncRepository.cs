using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskTodo.ApplicationCore.Entities;

namespace TaskTodo.ApplicationCore.Interfaces
{
    /// <summary>
    /// 带有泛型的仓储接口
    /// 定义仓储常用CRUD方法
    /// 
    /// 使用Specifications规约模式提取查询条件,
    /// 使用第三方组件Ardalis.Specification
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IAsyncRepository<T> where T : BaseEntity, IAggregateRoot
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);

        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);

        Task<int> CountAsync(ISpecification<T> spec);

        Task<T> FirstAsync(ISpecification<T> spec);
        Task<T> FirstOrDefaultAsync(ISpecification<T> spec);
    }
}
