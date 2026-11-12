using Project.Domain.Models;

namespace Project.Application.Interfaces.Repositories;

public interface IManagerNoteRepository
{
    Task<List<ManagerNote>> GetAllAsync();
    Task AddAsync(string text);

    Task UpdateAsync(long id, string newText);
    Task DeleteAsync(long id);
}