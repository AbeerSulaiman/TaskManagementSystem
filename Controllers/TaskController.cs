﻿using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<IEnumerable<TodoTask>>> GetTaskByCategory([FromQuery] Filter filter)
        {
            var todoTask = _dbContext.TodoTasks.AsQueryable();

            if (!string.IsNullOrEmpty(filter.Priority))
            {
                todoTask = todoTask.Where(x => x.Priority.ToString() == filter.Priority);
            }
            if (filter.DueDate.HasValue)
            {
                todoTask = todoTask.Where(x => x.DueDate == filter.DueDate);
            }

            var paginatedTasks = await todoTask.Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToListAsync();

            return Ok(paginatedTasks);
        }

        [HttpGet("sort")]
        public async Task<ActionResult<TodoTask>> GetTaskBySearchCategory([FromQuery] Sort sort)
        {

            var todoTask = await _dbContext.TodoTasks.ToListAsync();
            switch (sort.Category)
            {
                case "priority":
                    todoTask = sort.IsAsc ? todoTask.OrderBy(p => p.Priority).ToList() : todoTask.OrderByDescending(p => p.Priority).ToList();
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
        public async Task<ActionResult<TodoTask>> GetTaskById(int Id)
        {
            var todoTask = await _dbContext.TodoTasks.FirstOrDefaultAsync(x => x.Id == Id);
            if (todoTask == null)
            {
                return NotFound();
            }
            return Ok(todoTask);
        }
        [HttpGet("members/search/{Id}")]
        public async Task<ActionResult<TodoTask>> GetTaskByMemberId(int Id)
        {
            var todoTask = await _dbContext.TodoTasks.Where(x=> x.MemberId == Id).ToListAsync();
           
            if (todoTask == null)
            {
                return NotFound();
            }
            return Ok(todoTask);
        }

        [HttpPost]
        public async Task<ActionResult<TodoTaskDto>> AddTask(CreateTodoTaskDto todoTask)
        {
            var task = new TodoTask
            {
                Name = todoTask.Name,
                Description = todoTask.Description,
                TaskListId = todoTask.TaskListId,
                DueDate = todoTask.DueDate,
                Priority = todoTask.Priority,
                MemberId = todoTask.MemberId
            };

            _dbContext.TodoTasks.Add(task);
            await _dbContext.SaveChangesAsync();

            return Ok("task added successfully");
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
            task.Priority = todoTask.Priority;
            task.DueDate = todoTask.DueDate;
            task.TaskListId = todoTask.TaskListId;
            task.MemberId = todoTask.MemberId;
            await _dbContext.SaveChangesAsync();
            return Ok("Task Updated Successfully");

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