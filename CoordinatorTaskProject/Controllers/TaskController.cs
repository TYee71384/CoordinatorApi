using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoordinatorTaskProject.Data;
using CoordinatorTaskProject.DTO;
using CoordinatorTaskProject.Models.CoordinatorDb;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task = CoordinatorTaskProject.Models.CoordinatorDb.Task;

namespace CoordinatorTaskProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository repo;

        public TaskController(ITaskRepository repo)
        {
            this.repo = repo;
        }

        [HttpGet("{taskId}")]
        public async Task<ActionResult> GetTask(int taskId)
        {

            var task = await repo.GetData(taskId);
            if (task == null)
                return BadRequest("This is an invalid task");
            return Ok(task);
        }

           

        [HttpPost("{updateId}")]
        public async Task<ActionResult> AddTaskToUpdate(int updateId, Models.CoordinatorDb.Task task)
        {
            if (await repo.GetTaskFromAms(task.TaskId) != null)
            {
                task.UpdateId = updateId;
                if (await repo.CheckForDuplicateTask(task.TaskId, updateId))
                    return BadRequest("This task is already in the list");
                repo.Add(task);
                if (await repo.SaveAll())
                    return NoContent();
                return BadRequest("Cannot Save Task");
            }
            return BadRequest("Task does not exist in the AMS");
        }

        [HttpDelete("{taskNum}")]
        public async Task<ActionResult> RemoveTaskFromUpdate(int taskNum)
        {
            var task = await repo.GetTask(taskNum);
            repo.Delete(task);
           if(await repo.SaveAll())
            return NoContent();
            return BadRequest("Cannot Delete Task. Please try again");
        }

        [HttpPost("{updateId}/BulkAdd")]
        public async Task<ActionResult> BulkAddTasks(int updateId, IEnumerable<Task> tasks)
        {
            var errors = new List<Task>();
            foreach(var task in tasks)
            {
                if (await repo.GetTaskFromAms(task.TaskId) != null)
                {
                    if (await repo.CheckForDuplicateTask(task.TaskId, updateId) == false)
                    {
                        task.UpdateId = updateId;
                        repo.Add(task);
                    }
                }else
                {
                    errors.Add(task);
                }
               
            }

            if (await repo.SaveAll())
            {
                if(errors.Count > 0)
                {
                    return Ok(errors);
                }
                    return NoContent();
            }
                
            return BadRequest("There was a problem saving tasks, please try again");
        }
    }
}