using Domain;

namespace TodoApp.Data;

public static class InMemoryTodoStore
{
    public static List<Todo> Todos { get; } = new();

    // New collection for completed todos
    public static List<Todo> CompletedTodos { get; } = new();

    static InMemoryTodoStore()
    {
        Todos.AddRange(new[]
        {
            new Todo { Title = "Buy groceries", Description = "Milk, Bread, Eggs", IsCompleted = false, DueDate = DateTime.UtcNow.AddDays(1) },
            new Todo { Title = "Read book", Description = "Finish chapter 4", IsCompleted = true, DueDate = DateTime.UtcNow.AddDays(-2) },
            new Todo { Title = "Call Alice", Description = null, IsCompleted = false, DueDate = null }
        });

        // Move any pre-marked completed items into CompletedTodos collection
        var preCompleted = Todos.Where(t => t.IsCompleted).ToList();
        foreach (var t in preCompleted)
        {
            Todos.Remove(t);
            CompletedTodos.Add(t);
        }
    }

    public static void Add(Todo todo)
    {
        if (todo == null) throw new ArgumentNullException(nameof(todo));
        Todos.Insert(0, todo);
    }

    public static IReadOnlyList<Todo> GetAll() => Todos;

    public static Todo? GetById(Guid id) => Todos.FirstOrDefault(x => x.Id == id) ?? CompletedTodos.FirstOrDefault(x => x.Id == id);

    public static bool Update(Todo updated)
    {
        if (updated == null) throw new ArgumentNullException(nameof(updated));
        var existing = Todos.FirstOrDefault(x => x.Id == updated.Id) ?? CompletedTodos.FirstOrDefault(x => x.Id == updated.Id);
        if (existing == null) return false;

        // Update mutable fields only, keep Id and CreatedDate
        existing.Title = updated.Title;
        existing.Description = updated.Description;
        existing.IsCompleted = updated.IsCompleted;
        existing.DueDate = updated.DueDate;

        return true;
    }

    public static bool Remove(Guid id)
    {
        var t = Todos.FirstOrDefault(x => x.Id == id) ?? CompletedTodos.FirstOrDefault(x => x.Id == id);
        if (t == null) return false;

        if (Todos.Contains(t)) return Todos.Remove(t);
        return CompletedTodos.Remove(t);
    }

    public static IReadOnlyList<Todo> GetCompleted() => CompletedTodos;

    public static bool MarkCompleted(Guid id)
    {
        var t = Todos.FirstOrDefault(x => x.Id == id);
        if (t == null) return false;
        Todos.Remove(t);
        t.IsCompleted = true;
        CompletedTodos.Insert(0, t);
        return true;
    }
}