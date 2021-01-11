using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TaskTodo.ApplicationCore.Interfaces
{
    public interface ITokenClaimsService
    {
        Task<string> GetTokenAsync(string userName);
    }
}
