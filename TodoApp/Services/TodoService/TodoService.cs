using Domain;

namespace Services.TodoService;

public class TodoService : ITodoService
{
    private readonly List<Todo> _todos = new();
    private readonly List<Todo> _completed = new();

    public IReadOnlyCollection<Todo> Todos => _todos.AsReadOnly();
    public IReadOnlyCollection<Todo> CompletedTodos => _completed.AsReadOnly();

    public TodoService()
    {
        _todos.AddRange(new[]
        {
            new Todo { Title = "Buy groceries", Description = "Milk, Bread, Eggs", IsCompleted = false, DueDate = DateTime.UtcNow.AddDays(1) },
            new Todo { Title = "Read book", Description = "Finish chapter 4", IsCompleted = true, DueDate = DateTime.UtcNow.AddDays(-2) },
            new Todo { Title = "Call Alice", Description = null, IsCompleted = false, DueDate = null }
        });

        var preCompleted = _todos.Where(t => t.IsCompleted).ToList();
        foreach (var t in preCompleted)
        {
            _todos.Remove(t);
            _completed.Add(t);
        }
    }

    public Task<List<Todo>> GetAllAsync()
    {
        // May call real services or distributed cache to get the data and update the in-process State
        return Task.FromResult(_todos.ToList());
    }

    public Task<Todo?> GetByIdAsync(Guid id)
    {
        // May call real services or distributed cache to get the data and update the in-process State
        var todo = _todos.FirstOrDefault(x => x.Id == id) ?? _completed.FirstOrDefault(x => x.Id == id);
        return Task.FromResult(todo);
    }

    public Task<Todo> AddAsync(Todo todo)
    {
        // May call real services and distributed cache to add new data and update the in-process State
        if (todo == null) throw new ArgumentNullException(nameof(todo));
        _todos.Insert(0, todo);
        return Task.FromResult(todo);
    }

    public Task<bool> UpdateAsync(Todo todo)
    {
        // May call real services and distributed cache to update new data and update the in-process State
        if (todo == null) throw new ArgumentNullException(nameof(todo));
        var existing = _todos.FirstOrDefault(x => x.Id == todo.Id) ?? _completed.FirstOrDefault(x => x.Id == todo.Id);
        if (existing == null) return Task.FromResult(false);

        existing.Title = todo.Title;
        existing.Description = todo.Description;
        existing.IsCompleted = todo.IsCompleted;
        existing.DueDate = todo.DueDate;

        return Task.FromResult(true);
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        // May call real services and distributed cache to remove data and update the in-process State
        var t = _todos.FirstOrDefault(x => x.Id == id) ?? _completed.FirstOrDefault(x => x.Id == id);
        if (t == null) return Task.FromResult(false);
        if (_todos.Contains(t)) return Task.FromResult(_todos.Remove(t));
        return Task.FromResult(_completed.Remove(t));
    }

    public Task<List<Todo>> GetCompletedAsync()
    {
        return Task.FromResult(_completed.ToList());
    }

    // May call real services and distributed cache to update new data and update the in-process State
    public Task<bool> MarkCompletedAsync(Guid id)
    {
        var t = _todos.FirstOrDefault(x => x.Id == id);
        if (t == null) return Task.FromResult(false);
        _todos.Remove(t);
        t.IsCompleted = true;
        _completed.Insert(0, t);
        return Task.FromResult(true);
    }
}