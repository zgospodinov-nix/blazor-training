using Domain;

namespace Services.TodoService;

public interface ITodoService
{
    Task<List<Todo>> GetAllAsync();

    Task<List<Todo>> GetCompletedAsync();

    Task<Todo?> GetByIdAsync(Guid id);

    Task<Todo> AddAsync(Todo todo);

    Task<bool> UpdateAsync(Todo todo);

    Task<bool> DeleteAsync(Guid id);

    Task<bool> MarkCompletedAsync(Guid id);
}