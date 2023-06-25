using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Data;
using TaskManagementSystem.Dtos;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Controllers
{
    [ApiController]
    [Route("api/task-List")]
    public class TaskListController : Controller
    {
        private readonly TaskDBContext _dbContext;

        public TaskListController(TaskDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskList>>> GetAallTasksList()
        {
            return await _dbContext.TaskLists.Include(x => x.TodoTasks).ToListAsync();
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<TaskList>> GetTaskById(int Id)
        {
            var taskList = await _dbContext.TaskLists.Include(x => x.TodoTasks).FirstOrDefaultAsync(x => x.Id == Id);
            if (taskList == null)
            {
                return NotFound();
            }
            return Ok(taskList);
        }
        [HttpPost]
        public async Task<ActionResult<TaskListDto>> AddTaskList(CreateTaskListDto taskList)
        {
            var taskListItem = new TaskList
            {
                Name = taskList.Name,
                TodoTasks = taskList.TodoTasks
            };
            _dbContext.TaskLists.Add(taskListItem);
            await _dbContext.SaveChangesAsync();
            return Ok(taskListItem);
        }
        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateTask(int Id, UpdateTaskListDto taskListDto)
        {
            var taskList = await _dbContext.TaskLists.FirstOrDefaultAsync(x => x.Id == Id);
            if (taskList == null)
            {
                return NotFound();
            }
            taskList.Name = taskListDto.Name;
            taskList.TodoTasks = taskListDto.TodoTasks;

            await _dbContext.SaveChangesAsync();
            return Ok("Task List Updated Successfully");
        }
        [HttpDelete("{Id}")] 
        public async Task<IActionResult> DeleteTaskList([FromRoute] int Id)
        {
            var taskList = await _dbContext.TaskLists.FindAsync(Id);
            if (taskList == null)
            {
                return NotFound();
            }
            _dbContext.TaskLists.Remove(taskList);
            await _dbContext.SaveChangesAsync();
            return Ok("Deleted Successfully");

        }


    }
}