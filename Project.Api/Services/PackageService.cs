using Project.Api.Repositories.Interfaces;
using Project.Api.Services.Interfaces;
using Project.Domain.Enums;
using Project.Domain.Models;

namespace Project.Api.Services
{
    public class PackageService : IPackageService
    {
        private readonly IPackageRepository _packageRepository;

        private readonly IStorageLocationRepository _storageLocationRepository;

        private readonly IClientRepository _clientRepository;

        public PackageService(IPackageRepository packageRepository, IStorageLocationRepository storageLocationRepository, IClientRepository clientRepository)
        {
            _packageRepository = packageRepository;
            _storageLocationRepository = storageLocationRepository;
            _clientRepository = clientRepository;
        }

        public async Task<Package> RegisterPackageAsync(string number, string zone, string cell)
        {
            var package = await _packageRepository.GetByNumberAsync(number);

            if (package == null)
            {
                throw new Exception("Посылка с таким номером не найдена в базе ожидания.");
            }

            if (package.Status != PackageStatus.Expected)
            {
                throw new Exception("Эта посылка уже была принята ранее.");
            }

            package.Status = PackageStatus.AwaitingPickup;


            var location = await _storageLocationRepository.GetByZoneAndCellAsync(zone, cell);

            await _storageLocationRepository.UpdateAsync(location);

            if (location.IsOccupied) throw new Exception($"Ячейка {zone}-{cell} уже занята другой посылкой.");
            
            location.IsOccupied = true;
            location.Zone = zone;
            location.Cell = cell;
            
            package.StorageLocationId = location.Id;
            await _packageRepository.UpdateAsync(package);


            return package;


        }

        public async Task<Package?> SearchPackageByNumberAsync(string number)
        {
            var package = await _packageRepository.GetByNumberAsync(number);

            if (package == null)
            {
                throw new Exception("Посылка с таким номером не найдена на складе");
            }

            if (package.Status != PackageStatus.AwaitingPickup)
            {
                throw new Exception("Данная посылка не зарегестрирована на складе.");
            }

            var location = await _storageLocationRepository.GetByIdAsync(package.StorageLocationId.Value);


            package.StorageLocation = location;

            return package;
        }

        public async Task<List<Package>> SearchPackagesByPhone(string phone)
        {
            var client = await _clientRepository.GetByPhoneAsync(phone);

            if (client == null)
            {
                throw new Exception("Клиент с таким номером не найден.");
            }

            var packages = await _packageRepository.GetByClientIdAsync(client.Id);

            var resultPackages = new List<Package>();

            foreach (var package in packages)
            {
                if (package.Status != PackageStatus.AwaitingPickup)
                {
                    continue;
                }



                resultPackages.Add(package);


            }



            return resultPackages;
        }

        public async Task<List<Package>> SearchPackagesByName(string phone)
        {
            var client = await _clientRepository.GetByClientName(phone);

            if (client == null)
            {
                throw new Exception("Клиент с таким ФИО не найден.");
            }

            var packages = await _packageRepository.GetByClientIdAsync(client.Id);

            var resultPackages = new List<Package>();

            foreach (var package in packages)
            {
                if (package.Status != PackageStatus.AwaitingPickup)
                {
                    continue;
                }


                resultPackages.Add(package);

            }


            return resultPackages;

        }

        public async Task<Package?> ExtraditionPackageAsync(string number)
        {
            var package = await _packageRepository.GetByNumberAsync(number);

            if (package == null)
            {
                throw new Exception("Посылка с таким номером не найдена на складе");
            }

            if (package.Status != PackageStatus.Expected)
            {
                throw new Exception("Эта посылка уже была принята ранее.");
            }

            package.Status = PackageStatus.Issued;

            var location = await _storageLocationRepository.GetByIdAsync(package.StorageLocationId.Value);

            if (location == null) throw new Exception("Эта посылка не зарегистрирована на складе");


            location.IsOccupied = false;

            package.StorageLocationId = null;

            return package;




        }

    }
}
