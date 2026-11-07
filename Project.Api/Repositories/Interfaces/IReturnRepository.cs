using Project.Domain.Models;

namespace Project.Api.Repositories.Interfaces
{
    public interface IReturnRepository
    {
        
        Task AddAsync(Return returnRecord);

       
        
    }
}
