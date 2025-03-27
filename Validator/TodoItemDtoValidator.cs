using FluentValidation;
using AspNetCoreTodo.DTOs; 

public class TodoItemDtoValidator : AbstractValidator<TodoItemDTO>
{
    public TodoItemDtoValidator()
    {
        // Validate that Title is not empty and has a minimum length of 11 characters
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MinimumLength(11).WithMessage("Title must be more than 10 characters.");
    }
}
