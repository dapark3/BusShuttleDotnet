using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMvc.Service
{
    public interface IUserService
    {
        Task<string> GetUserEmail(HttpContext context);
        Task UpdateAccountActivation(string id, bool v);
    }
}