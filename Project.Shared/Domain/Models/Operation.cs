using Project.Domain.Enums;

namespace Project.Domain.Models;

public class Operation
{
    public long Id { get; set; }

    public long PackageId { get; set; }
    // Навигационное свойство: EF Core поймет, что здесь связь с посылкой
    public Package? Package { get; set; }

    public long EmployeeId { get; set; }
    // Навигационное свойство: EF Core поймет, что здесь связь с сотрудником
    public Employee? Employee { get; set; }

    public OperationType OperationType { get; set; }
    
    public DateTime Timestamp { get; set; }
}