using Project.Domain.Models;

namespace Project.Api.Repositories.Interfaces;

public interface IOperationRepository
{
   
    Task AddAsync(Operation operation);

    
    Task<List<Operation>> GetHistoryAsync(DateTime? fromDate, DateTime? toDate);
}