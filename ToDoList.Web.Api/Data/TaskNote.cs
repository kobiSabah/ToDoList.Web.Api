using System;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.Web.Api.Data
{
    public class TaskNote
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public bool IsComplete { get; set; }
    }
}
