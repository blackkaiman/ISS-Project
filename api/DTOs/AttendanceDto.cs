using System;
using System.ComponentModel.DataAnnotations;
using YonderfulApi.DTOs;

namespace YonderfulApi.DTOs
{
    public class AttendanceDto
    {
        [Required]
        public int EventId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public DateTime JoiningDate { get; set; }
        
    }
}