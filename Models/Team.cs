﻿namespace TaskManagementSystem.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Member> Members { get; set; }   
    }
}
