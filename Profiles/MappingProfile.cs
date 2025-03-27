using AutoMapper;
using AspNetCoreTodo.Models;
using AspNetCoreTodo.DTOs;

namespace AspNetCoreTodo.MappingProfiles
{
    public class TodoMappingProfile : Profile
    {
        public TodoMappingProfile()
        {
            // Map TodoItem to TodoItemDTO
            CreateMap<TodoItem, TodoItemDTO>();

            // Map TodoCreateDTO to TodoItem
            CreateMap<TodoCreateDTO, TodoItem>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))  // Automatically generate a new Id
                .ForMember(dest => dest.IsDone, opt => opt.MapFrom(src => false));  // Default to IsDone being false
        }
    }
}
