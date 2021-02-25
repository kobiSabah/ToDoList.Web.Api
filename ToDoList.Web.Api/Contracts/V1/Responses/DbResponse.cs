using System;
using System.Collections.Generic;

namespace ToDoList.Web.Api.Contracts.V1.Responses
{
    public class DbResponse<T>
    {
        public bool IsSuccessed { get; set; }
        public IEnumerable<string> Errors { get; set; }

        public T Context { get; set; }
    }
}
