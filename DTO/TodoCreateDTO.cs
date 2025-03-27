namespace AspNetCoreTodo.DTOs
{
    public class TodoCreateDTO
    {
        public required string Title { get; set; }
        public DateTimeOffset? DueAt { get; set; }
    }
}
