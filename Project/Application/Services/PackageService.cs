
using Project.Application.Interfaces.Repositories;
using Project.Application.Interfaces.Services;
using Project.Domain.Enums;
using Project.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Application.Services
{
    public class PackageService : IPackageService
    {
        private readonly IPackageRepository _packageRepository;

        public PackageService(IPackageRepository packageRepository)
        {
            _packageRepository = packageRepository;
        }

        public async Task<Package?> RegistrAsync(string number, string zone, string cell)
        {
            return await _packageRepository.RegisterPackageAsync(number, zone, cell);
        }

        public async Task<Package?> SearchByNumberAsync(string number)
        {
            return await _packageRepository.SearchByNumberAsync(number);
        }

        public async Task<List<Package>> SearchByPhoneAsync(string phone)
        {
            return await _packageRepository.SearchByPhoneAsync(phone);
        }

        public async Task<List<Package>> SearchByNameAsync(string name)
        {
            return await _packageRepository.SearchByNameAsync(name);
        }

        public async Task<Package?> ExtraditionPackageAsync(string number)
        {
            return await _packageRepository.ExtraditionPackageAsync(number);
        }

    }   
}
