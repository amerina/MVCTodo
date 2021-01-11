using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TaskTodo.ApplicationCore.Entities;

namespace Infrastructure.Data.Config
{
    /// <summary>
    /// 
    /// </summary>
    public class TodoItemConfiguration : IEntityTypeConfiguration<TodoItem>
    {
        public void Configure(EntityTypeBuilder<TodoItem> builder)
        {
            builder.OwnsOne(o => o.Task, a =>
            {
                a.WithOwner();

                a.Property(a => a.Title)
                    .HasMaxLength(18)
                    .IsRequired();

                a.Property(a => a.Description)
                    .HasMaxLength(180)
                    .IsRequired();
            });
        }
    }
}
