
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoList.Web.Api.Contracts.V1.Responses;
using ToDoList.Web.Api.Data;

namespace ToDoList.Web.Api.Service
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskNote>> GetAllAsync();

        Task<bool> CreateAsync(TaskNote task);

        Task<TaskNote> GetTaskAsync(string taskId);

        Task<DbResponse<TaskNote>> CompleteTaskAsync(string taskId);

        Task<DbResponse<TaskNote>> DeleteTaskAsync(string taskId);
    }
}
