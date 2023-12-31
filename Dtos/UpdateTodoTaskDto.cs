﻿using TaskManagementSystem.Models;

namespace TaskManagementSystem.Dtos
{
    public class UpdateTodoTaskDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public int Priority { get; set; }
        public int Status { get; set; }
        public int TaskListId { get; set; }
        public int MemberId { get; set; }

    }
}
