namespace Project.Domain.Models;

public class ManagerNote
{
    public long Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public long EmployeeId { get; set; } 
    public Employee? Employee { get; set; } 
}