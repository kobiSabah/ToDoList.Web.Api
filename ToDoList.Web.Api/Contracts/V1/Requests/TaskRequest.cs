using System;

namespace ToDoList.Web.Api.Contracts.V1.Requests
{
    public class TaskRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}
