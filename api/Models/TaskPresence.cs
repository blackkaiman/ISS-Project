using System;

namespace YonderfulApi.Models
{
    public class TaskPresence
    {
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

    }
}