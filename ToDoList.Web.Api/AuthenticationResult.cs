using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Web.Api
{
    public class AuthenticationResult<T>
    {
        public bool IsSuccessed { get; set; }
        public IEnumerable<string> Errors { get; set; }

        public T Context { get; set; }
    }
}
