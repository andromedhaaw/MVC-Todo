using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreTodo.Services;
using AspNetCoreTodo.Models;
using AspNetCoreTodo.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using FluentValidation.Results;

namespace AspNetCoreTodo.Controllers
{
    [Authorize]
    public class TodoController : Controller
    {
        private readonly ITodoItemService _todoItemService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;

        public TodoController(ITodoItemService todoItemService, UserManager<IdentityUser> userManager, IMapper mapper)
        {
            _todoItemService = todoItemService;
            _userManager = userManager;
            _mapper = mapper;
        }

        // Index method to display incomplete to-do items
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            var items = await _todoItemService.GetIncompleteItemAsync(currentUser);

            // Map the TodoItem list to TodoItemDTO list
            var itemsDTO = _mapper.Map<List<TodoItemDTO>>(items);

            var model = new TodoViewModel()
            {
                Items = itemsDTO
            };

            return View(model);
        }

        // AddItem method to add a new to-do item
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddItem(TodoCreateDTO newItem)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            if (!ModelState.IsValid) // Check for validation errors
            {
                var items = await _todoItemService.GetIncompleteItemAsync(currentUser);
                var itemsDTO = _mapper.Map<List<TodoItemDTO>>(items);
                var model = new TodoViewModel { Items = itemsDTO, NewItem = newItem }; // Pass NewItem back to the view
                return View("Index", model); // Return the view with validation errors
            }

            var todoItem = _mapper.Map<TodoItem>(newItem); // Map the DTO to the model
            var successful = await _todoItemService.AddItemAsync(todoItem, currentUser);

            if (!successful)
            {
                return BadRequest(new { error = "Could not add item." });
            }

            return RedirectToAction("Index");
        }

        // Mark item as done
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkDone(Guid id)
        {
            if (id == Guid.Empty)
            {
                return RedirectToAction("Index");
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            var successful = await _todoItemService.MarkDoneAsync(id, currentUser);

            if (!successful)
            {
                return BadRequest("Could not mark item as done.");
            }

            return RedirectToAction("Index");
        }

        // Delete item
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteItem(Guid id)
        {
            if (id == Guid.Empty)
            {
                return RedirectToAction("Index");
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return Challenge();
            }

            var successful = await _todoItemService.DeleteItemAsync(id, currentUser);
            if (!successful)
            {
                return BadRequest(new { error = "Could not delete item." });
            }

            return RedirectToAction("Index");
        }
    }
}
