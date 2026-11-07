using Project.Domain.Models;

namespace Project.Api.Repositories.Interfaces
{
    public interface IReturnReasonRepository
    {
        Task<List<ReturnReason>> GetAllActiveAsync();

        Task<ReturnReason?> GetById(long id);
        
    }
}
