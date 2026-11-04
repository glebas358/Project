using Project.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Application.Interfaces.Repositories
{
    public interface IClientRepository
    {
        Task<Client?> GetIdByPhoneAsync(string phone);
    }
}
