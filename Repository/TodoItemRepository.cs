using AspNetCoreTodo.Data;
using AspNetCoreTodo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreTodo.Repository
{
    public class TodoItemRepository : ITodoItemRepository
    {
        private readonly ApplicationDbContext _context;

        public TodoItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TodoItem>> GetAllAsync()
        {
            return await _context.TodoItems.ToListAsync(); // Ensure this line is asynchronous
        }

        public async Task<TodoItem> GetByIdAsync(Guid todoItemId)
        {
            return await _context.TodoItems.FindAsync(todoItemId); // Ensure this is asynchronous
        }

        public async Task AddAsync(TodoItem todoItem)
        {
            await _context.TodoItems.AddAsync(todoItem); // Ensure AddAsync is used for async operation
        }

        public async Task UpdateAsync(TodoItem todoItem)
        {
            _context.TodoItems.Update(todoItem); // The update operation should still be synchronous, but ensure it's handled correctly
        }

        public async Task DeleteAsync(Guid todoItemId)
        {
            var todoItem = await _context.TodoItems.FindAsync(todoItemId);
            if (todoItem != null)
            {
                _context.TodoItems.Remove(todoItem); // Remove async operation handled correctly
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync(); // Ensure SaveChangesAsync is awaited
        }
    }
}
