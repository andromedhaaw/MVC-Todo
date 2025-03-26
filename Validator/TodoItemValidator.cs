using FluentValidation;
 using AspNetCoreTodo.Models;
 
 public class TodoItemValidator : AbstractValidator<TodoItem>
 {
     public TodoItemValidator()
     {
         RuleFor(x => x.Title)
             .NotEmpty().WithMessage("Title is required.")
             .MinimumLength(11).WithMessage("Title must be more than 10 characters.");
     }
 }