using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace dotnet_api_whiteapp.Models
{
    public class ToDoItem
    {
        [Required]
        public int Id { get; set; }

        public DateTime Date { get; set; }

        [DefaultValue("To Do")]
        public string Title { get; set; }

    }
}
