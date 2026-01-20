using Domain;

namespace TodoApp.Data;

public static class InMemoryTodoStore
{
    public static List<Todo> Todos { get; } = new();

    static InMemoryTodoStore()
    {
        Todos.AddRange(new[]
        {
            new Todo { Title = "Buy groceries", Description = "Milk, Bread, Eggs", IsCompleted = false, DueDate = DateTime.UtcNow.AddDays(1) },
            new Todo { Title = "Read book", Description = "Finish chapter 4", IsCompleted = true, DueDate = DateTime.UtcNow.AddDays(-2) },
            new Todo { Title = "Call Alice", Description = null, IsCompleted = false, DueDate = null }
        });
    }

    public static void Add(Todo todo)
    {
        if (todo == null) throw new ArgumentNullException(nameof(todo));
        Todos.Insert(0, todo);
    }

    public static IReadOnlyList<Todo> GetAll() => Todos;

    public static bool Remove(Guid id)
    {
        var t = Todos.FirstOrDefault(x => x.Id == id);
        if (t == null) return false;
        return Todos.Remove(t);
    }
}