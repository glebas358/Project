using Project.Domain.Models;


namespace Project.Application.Interfaces.Repositories
{
    public interface IOperationRepository
    {
        Task<List<Operation>> GetHistoryAsync(DateTime? fromDate, DateTime? toDate);
    }
}
