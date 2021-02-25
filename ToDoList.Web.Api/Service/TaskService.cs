using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoList.Web.Api.Contracts.V1.Responses;
using ToDoList.Web.Api.Data;

namespace ToDoList.Web.Api.Service
{
    public class TaskService : ITaskService
    {
        private ApplicationDbContext m_DbContext;

        public TaskService(ApplicationDbContext dbContext)
        {
            m_DbContext = dbContext;
        }

        public async Task<IEnumerable<TaskNote>> GetAllAsync() => await m_DbContext.Tasks.ToListAsync(); 
        

        public async Task<bool> CreateAsync(TaskNote task)
        {
            await m_DbContext.Tasks.AddAsync(task);

            int changes = await m_DbContext.SaveChangesAsync();

            return changes > 0;
        }

        public async Task<TaskNote> GetTaskAsync(string taskId) => await m_DbContext.Tasks.SingleOrDefaultAsync(t => t.Id.ToString().Equals(taskId));

        public async Task<DbResponse<TaskNote>> CompleteTaskAsync(string taskId)
        {
            TaskNote taks = await GetTaskAsync(taskId);
            DbResponse<TaskNote> result = new DbResponse<TaskNote>();

            if (taks == null)
            {
                result.IsSuccessed = false;
                result.Errors = new[] { "Task not found" };
            }
            else if (taks.IsComplete)
            {
                result.IsSuccessed = false;
                result.Errors = new[] { "Task already completed" };
            }
            else
            {
                taks.IsComplete = true;
                await m_DbContext.SaveChangesAsync();
                result.IsSuccessed = true;
            }

            return result;
        }

        public async Task<DbResponse<TaskNote>> DeleteTaskAsync(string taskId)
        {
            TaskNote taks = await GetTaskAsync(taskId);
            DbResponse<TaskNote> result = new DbResponse<TaskNote>();

            if (taks == null)
            {
                result.IsSuccessed = false;
                result.Errors = new[] { "Task not found" };
            }
            else
            {
                m_DbContext.Tasks.Remove(taks);
                await m_DbContext.SaveChangesAsync();
                result.IsSuccessed = true;
            }

            return result;
        }
    }
}
