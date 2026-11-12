using Project.Domain.Enums;

namespace Project.Domain.Models;

public class Operation
{
    public long Id { get; set; }

    public long PackageId { get; set; }
    
    public Package? Package { get; set; }

    public long EmployeeId { get; set; }
    
    public Employee? Employee { get; set; }

    public OperationType OperationType { get; set; }
    
    public DateTime Timestamp { get; set; }

    [System.Text.Json.Serialization.JsonIgnore]
    public string OperationTypeRussian
    {
        get
        {
            return OperationType switch
            {
                OperationType.Accept => "Принята",
                OperationType.Issue => "Выдана",
                OperationType.Return => "Возврат",
                _ => "Неизвестно"
            };
        }
    }
}