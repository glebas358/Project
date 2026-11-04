using Project.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<Employee?> LoginAsync(string login, string password);
    }
}
