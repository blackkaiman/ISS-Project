using System;
using System.ComponentModel.DataAnnotations;
using YonderfulApi.DTOs;

namespace YonderfulApi.DTOs
{
    public class TaskPresenceDto
    {
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public int UserId { get; set; }
        
    }
}