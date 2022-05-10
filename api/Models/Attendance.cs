using System;

namespace YonderfulApi.Models
{
    public class Attendance
    {
        public int EventId { get; set; }
        public Event Event { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime JoiningDate { get; set; }

    }
}