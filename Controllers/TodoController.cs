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

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddItem(TodoCreateDTO newItem)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            if (!ModelState.IsValid)
            {
                var items = await _todoItemService.GetIncompleteItemAsync(currentUser);
                var itemsDTO = _mapper.Map<List<TodoItemDTO>>(items);
                var model = new TodoViewModel { Items = itemsDTO };
                return View("Index", model);
            }

            var todoItem = _mapper.Map<TodoItem>(newItem); // Map DTO to model
            var successful = await _todoItemService.AddItemAsync(todoItem, currentUser);

            if (!successful)
            {
                return BadRequest(new { error = "Could not add item." });
            }

            return RedirectToAction("Index");
        }

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
