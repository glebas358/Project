using Project.Domain.Models;

namespace Project.Api.Repositories.Interfaces
{
    public interface IManagerNoteRepository
    {

        Task AddAsync(ManagerNote note);

        Task UpdateAsync(ManagerNote note);
        Task DeleteAsync(ManagerNote note);

        Task<List<ManagerNote>> GetAllAsync();

        Task<ManagerNote?> GetByIdAsync(long id);
    }
}
