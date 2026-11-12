using Project.Domain.Models;

namespace Project.Api.Services.Interfaces
{
    public interface IManagerNoteService
    {
        Task AddAsync(string text, long employeeId);

        Task<List<ManagerNote>> GetAllAsync();

        Task UpdateAsync(long id, string newText, long EmployeeId);
        Task DeleteAsync(long id, long EmployeeId);
    }
}
