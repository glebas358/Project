using Project.Domain.Models;

namespace Project.Api.Services.Interfaces;

public interface IOperationService
{
    Task<List<Operation>> GetHistoryAsync(DateTime? fromDate, DateTime? toDate);
}