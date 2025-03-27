using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AspNetCoreTodo.Models;
using Microsoft.AspNetCore.Identity;


namespace AspNetCoreTodo.Repository
{
    public interface ITodoItemRepository
    {
        Task<IEnumerable<TodoItem>> GetAllAsync();            // Retrieve all TodoItems
        Task<TodoItem> GetByIdAsync(Guid todoItemId);         // Retrieve a single TodoItem by ID
        Task AddAsync(TodoItem todoItem);                      // Insert a new TodoItem
        Task UpdateAsync(TodoItem todoItem);                   // Update an existing TodoItem
        Task DeleteAsync(Guid todoItemId);                     // Delete a TodoItem by ID
        Task SaveAsync();                                      // Save changes to the database
    }
}
