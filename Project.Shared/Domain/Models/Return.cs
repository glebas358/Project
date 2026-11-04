namespace Project.Domain.Models;

public class Return
{
    public long Id { get; set; }

    public long PackageId { get; set; }
   
    public Package? Package { get; set; }

    public long EmployeeId { get; set; }
   
    public Employee? Employee { get; set; }

    public long ReturnReasonId { get; set; }
 
    public ReturnReason? ReturnReason { get; set; }

    public DateTime ReturnDate { get; set; }
}