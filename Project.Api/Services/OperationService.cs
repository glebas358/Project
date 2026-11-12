using Project.Api.Repositories.Interfaces;
using Project.Api.Services.Interfaces;
using Project.Domain.Models;

namespace Project.Api.Services;

public class OperationService : IOperationService
{
    private readonly IOperationRepository _operationRepository;

    public OperationService(IOperationRepository operationRepository)
    {
        _operationRepository = operationRepository;
    }

    public async Task<List<Operation>> GetHistoryAsync(DateTime? fromDate, DateTime? toDate)
    {
        var notes = await _operationRepository.GetHistoryAsync(fromDate, toDate);

        if (notes.Count == 0)
            throw new AppException("Записи за этот период не найдены.", 404);

        return notes;

        //return await _operationRepository.GetHistoryAsync(fromDate, toDate);
    }
}