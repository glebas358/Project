using Project.Domain.Models;

namespace Project.Api.Repositories.Interfaces
{
    public interface IClientRepository
    {
        Task<Client?> GetByPhoneAsync(string phone);

        Task<Client?> GetByClientName(string name);
    }
}
