using Domain;

namespace TodoApp.Services.TodoService;

public interface ITodoService
{
    public event Action? OnChange;

    public IReadOnlyCollection<Todo> Todos { get; }
    public IReadOnlyCollection<Todo> CompletedTodos { get; }

    Task<List<Todo>> GetAllAsync();

    Task<List<Todo>> GetCompletedAsync();

    Task<Todo?> GetByIdAsync(Guid id);

    Task<Todo> AddAsync(Todo todo);

    Task<bool> UpdateAsync(Todo todo);

    Task<bool> DeleteAsync(Guid id);

    Task<bool> MarkCompletedAsync(Guid id);
}