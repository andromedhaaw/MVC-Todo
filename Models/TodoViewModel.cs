using AspNetCoreTodo.DTOs;

namespace AspNetCoreTodo.Models
{
    public class TodoViewModel
    {
        
        public List<TodoItemDTO> Items { get; set; }
        public TodoCreateDTO NewItem { get; set; }
        
    }
}