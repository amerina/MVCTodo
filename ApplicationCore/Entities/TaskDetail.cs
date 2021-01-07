using System;
using System.Collections.Generic;
using System.Text;

namespace TaskTodo.ApplicationCore.Entities
{
    /// <summary>
    /// TaskDetail is a ValueObject
    /// </summary>
    public class TaskDetail
    {
        private TaskDetail() {
        
        }

        public TaskDetail(string title,string description)
        {
            Title = title;
            Description = description;
        }
       public string Title { get; private set; }

        public string Description { get; private set; }

       
    }
}
