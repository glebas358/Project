
using Project.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Application.Interfaces.Services
{
    public interface IPackageService
    {

        Task<Package?> RegistrAsync(string number, string zone, string cell);

        Task<Package?> SearchByNumberAsync(string number);

        Task<List<Package>> SearchByPhoneAsync(string phone);

        Task<List<Package>> SearchByNameAsync(string name);




    }
}
