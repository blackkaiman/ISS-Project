using System;

namespace YonderfulApi.Models
{
    public class TaskPresence
    {
        public int TaskId { get; set; }
        public TaskEmp Task { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

    }
}