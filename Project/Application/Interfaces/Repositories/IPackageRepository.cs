using Project.Domain.Enums;
using Project.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Application.Interfaces.Repositories
{
    public interface IPackageRepository
    {
        
        Task<Package?> RegisterPackageAsync(string number, string zone, string cell);

        
        Task<Package?> SearchByNumberAsync(string number);

        Task<List<Package>> SearchByPhoneAsync(string phone);

        Task<List<Package>> SearchByNameAsync(string name);


        Task<List<Package>> GetByStatusAsync(PackageStatus status);

        Task<Package> ExtraditionPackageAsync(string number);

        Task UpdateAsync(Package package);

        
    }
}
