using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoList.Web.Api.Contracts.V1.Requests;
using ToDoList.Web.Api.Data;
using ToDoList.Web.Api.Service;

namespace ToDoList.Web.Api.Controllers
{
    public class TaskController : Controller
    {
        ITaskService m_TaskService;

        public TaskController(ITaskService taskService)
        {
            m_TaskService = taskService;
        }

        [HttpGet(ApiRoutes.NoteTask.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<TaskNote> tasks = await m_TaskService.GetAllAsync();

            return Ok(tasks);
        }

        [HttpPut(ApiRoutes.NoteTask.Create)]
        public async Task<IActionResult> Create([FromBody] TaskRequest taskRequest)
        {
            TaskNote task = new TaskNote
            {
                Title = taskRequest.Title,
                Description = taskRequest.Description,
                Date = taskRequest.Date
            };

            bool isSuccesss = await m_TaskService.CreateAsync(task);

            if(isSuccesss)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost(ApiRoutes.NoteTask.Get)]
        public async Task<IActionResult> Get([FromRoute] string taskId)
        {
            var result = await m_TaskService.GetTaskAsync(taskId);
            if(result == null)
            {
                return NotFound($"Task number : {taskId} was not found.");
            }

            return Ok(result);
        }

        [HttpPatch(ApiRoutes.NoteTask.Complete)]
        public async Task<IActionResult> Complete([FromRoute] string taskId)
        {
            var result = await m_TaskService.CompleteTaskAsync(taskId);
            if (!result.IsSuccessed)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result);
        }

        [HttpDelete(ApiRoutes.NoteTask.Delete)]
        public async Task<IActionResult> Delete([FromRoute] string taskId)
        {
            var result = await m_TaskService.DeleteTaskAsync(taskId);
            if (!result.IsSuccessed)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result);
        }

    }
}
