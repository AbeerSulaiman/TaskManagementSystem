using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using System.Diagnostics;
using System.Xml.Linq;
using TaskManagementSystem.Data;
using TaskManagementSystem.Dtos;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TaskController : Controller
    {
        private readonly TaskDBContext _dbContext;
        public TaskController(TaskDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoTaskDto>>> GetTaskByCategory([FromQuery] Filter filter)
        {
            var todoTask = _dbContext.TodoTasks.AsQueryable();

            if (filter.Status.GetValueOrDefault() == 0)
            {
                todoTask = todoTask.Where(x => (int)x.Priority == filter.Priority);
            }
            if (filter.Status >= 0)
            {
                todoTask = todoTask.Where(x => (int)x.Status == filter.Status);
            }
            if (filter.DueDate.HasValue)
            {
                todoTask = todoTask.Where(x => x.DueDate.Date == filter.DueDate.Value.Date);
            }

            var paginatedTasks = await todoTask.Page(new CustomQueryParameters {
                Page = 1 , PageCount=100})
            .ToListAsync();
            List<TodoTaskDto> tasksResponse = new List<TodoTaskDto>();
            foreach (var todoTask1 in paginatedTasks)
            {
                tasksResponse.Add(new TodoTaskDto
                {
                    Id = todoTask1.Id,
                    Name = todoTask1.Name,
                    DueDate = todoTask1.DueDate,
                    Description = todoTask1.Description,
                    TaskListId = todoTask1.TaskListId,
                    MemberId = todoTask1.MemberId,
                    Priority = (int)todoTask1.Priority,
                    Status = (int)todoTask1.Status,
                    PriorityString = todoTask1.Priority.ToString(),
                    StatusString = todoTask1.Status.ToString(),
                });
           
        
            }

            return Ok(tasksResponse);
        }

        [HttpGet("sort")]
        public async Task<ActionResult<TodoTaskDto>> GetTaskBySearchCategory([FromQuery] Sort sort)
        {

            var todoTask = await _dbContext.TodoTasks.ToListAsync();
            switch (sort.Category)
            {
                case "priority":
                    todoTask = sort.IsAsc ? todoTask.OrderBy(p => (int)p.Priority).ToList() : todoTask.OrderByDescending(p => p.Priority).ToList();
                    break;
                case "status":
                    todoTask = sort.IsAsc ? todoTask.OrderBy(p => (int)p.Status).ToList() : todoTask.OrderByDescending(p => p.Status).ToList();
                    break;
                case "duDate":
                    todoTask = sort.IsAsc ? todoTask.OrderBy(p => p.DueDate).ToList() : todoTask.OrderByDescending(p => p.DueDate).ToList();
                    break;
                default:
                    break;
            }
            if (todoTask == null)
            {
                return NotFound();
            }
            return Ok(todoTask);
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<TodoTaskDto>> GetTaskById(int Id)
        {
            var todoTask = await _dbContext.TodoTasks.FirstOrDefaultAsync(x => x.Id == Id);
            if (todoTask == null)
            {
                return NotFound();
            }
            return Ok(todoTask);
        }
        [HttpGet("members/search/{Id}")]
        public async Task<ActionResult<TodoTaskDto>> GetTaskByMemberId(int Id)
        {
            var todoTask = await _dbContext.TodoTasks.Where(x=> x.MemberId == Id).ToListAsync();
           
            if (todoTask == null)
            {
                return NotFound();
            }
            return Ok(todoTask);
        }

        [HttpPost]
        public async Task<ActionResult<TodoTask>> AddTask(CreateTodoTaskDto todoTask)
        {
            var task = new TodoTask
            {
                Name = todoTask.Name,
                Description = todoTask.Description,
                TaskListId = todoTask.TaskListId,
                DueDate = todoTask.DueDate,
                Priority = (Priority)todoTask.Priority,
                Status = (Status)todoTask.Status,
                MemberId = todoTask.MemberId
            };

            _dbContext.TodoTasks.Add(task);
            await _dbContext.SaveChangesAsync();

            return Ok(task);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateTask(int Id, UpdateTodoTaskDto todoTask)
        {
            var task = await _dbContext.TodoTasks.FirstOrDefaultAsync(x => x.Id == Id);
            if (task == null)
            {
                return NotFound();
            }
            task.Name = todoTask.Name;
            task.Description = todoTask.Description;
            task.Priority = (Priority)todoTask.Priority;
            task.Status = (Status)todoTask.Status;
            task.DueDate = todoTask.DueDate;
            task.TaskListId = todoTask.TaskListId;
            task.MemberId = todoTask.MemberId;
            await _dbContext.SaveChangesAsync();
            return Ok(task);

        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteTaskList([FromRoute] int Id)
        {
            var todoTask = await _dbContext.TodoTasks.FindAsync(Id);
            if (todoTask == null)
            {
                return NotFound();
            }
            _dbContext.TodoTasks.Remove(todoTask);
            await _dbContext.SaveChangesAsync();
            return Ok("Deleted Successfully");

        }
    }
}
