using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using TaskTodo.ApplicationCore.Entities;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
        }

        /// <summary>
        /// 为了把“数据库上下文中的改动”反映到数据库里，你需要创建一个 变更(migration)。
        /// 变更按时间记录着数据库结构的变化。它们使以下的操作成为可能：
        /// 撤销(回滚)一部分修改，或创建一个新的数据库——与原有数据库结构一致。
        /// 有了变更，你有一个完整的数据库历史，记录着对数据库的修改，例如添加或删除字段（以及整个表）。
        /// dotnet ef migrations add AddItems创建变更
        ///
        /// dotnet ef database update应用变更
        /// 
        ///  dotnet ef migrations add AddItemUserId创建变更
        /// </summary>
        public DbSet<TodoItem> Items { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //加载TodoItemConfiguration
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
