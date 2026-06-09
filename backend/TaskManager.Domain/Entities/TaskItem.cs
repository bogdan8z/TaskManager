namespace TaskManager.Domain.Entities;
public class TaskItem
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public bool IsCompleted { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; } = default!;

}