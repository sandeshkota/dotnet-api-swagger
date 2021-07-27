using Swashbuckle.AspNetCore.Annotations;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace dotnet_api_swagger.Models
{
    [SwaggerSchema(Required = new[] { "Title" })]
    public class ToDoItem
    {
        [Required]
        [SwaggerSchema("The Id for the Todo item", Required = new string[] { "0", "1", "2", "3", "...", "100" })]
        public int Id { get; set; }


        [SwaggerSchema("The date for the Todo item", Format = "date")]
        public DateTime Date { get; set; }


        [SwaggerSchema("The title for the Todo item", Nullable = true, ReadOnly = false, Title = "Todo-item-title", WriteOnly = false)]
        public string Title { get; set; }

    }
}
