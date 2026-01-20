using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Todo
{
    public Guid Id { get; init; } = Guid.NewGuid();

    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;

    [StringLength(2000)]
    public string? Description { get; set; }

    public bool IsCompleted { get; set; }

    public DateTime CreatedDate { get; init; } = DateTime.UtcNow;

    public DateTime? DueDate { get; set; }

    public void ToggleCompletion() => IsCompleted = !IsCompleted;
}