﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskTodo.ViewModels
{
    public class NewTodoItem
    {
        [Required]
        public string Title { get; set; }
    }
}
