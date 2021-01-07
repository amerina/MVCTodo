using System;
using System.Collections.Generic;
using System.Text;

namespace TaskTodo.ApplicationCore.Exceptions
{
    /// <summary>
    /// 自定义异常
    /// 一般情况下，只应捕获你知道如何从其恢复的异常。 因此，应始终指定派生自 System.Exception 的对象
    /// </summary>
    public class TodoItemNotFoundException : Exception
    {
        public TodoItemNotFoundException(int id) : base($"No todoItem found with id {id}")
        {

        }

        protected TodoItemNotFoundException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {

        }

        public TodoItemNotFoundException(string message) : base(message)
        {

        }

        public TodoItemNotFoundException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
