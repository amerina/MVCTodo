using Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskTodo.ViewModels
{
    public class ManageUsersViewModel
    {
        public ApplicationUser[] Administrators { get; set; }

        public ApplicationUser[] Everyone { get; set; }
    }
}
