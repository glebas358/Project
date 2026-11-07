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

        private readonly IReturnReasonRepository _returnReasonRepository;

        private readonly IReturnRepository _returnRepository;

        public PackageService(IPackageRepository packageRepository, IStorageLocationRepository storageLocationRepository, IClientRepository clientRepository, IReturnReasonRepository returnReasonRepository, IReturnRepository returnRepository)
        {
            _packageRepository = packageRepository;
            _storageLocationRepository = storageLocationRepository;
            _clientRepository = clientRepository;
            _returnReasonRepository = returnReasonRepository;
            _returnRepository = returnRepository;
        }

        public async Task<Package> RegisterPackageAsync(string number, string zone, string cell)
        {
            var package = await _packageRepository.GetByNumberAsync(number);

            if (package == null)
                throw new AppException("Посылка не найдена.", 404);

            if (package.Status != PackageStatus.Expected)
                throw new AppException($"Нельзя зарегестрировать посылку со статусом {package.Status}", 409);
            

            package.Status = PackageStatus.AwaitingPickup;

            package.ArrivalDate = DateTime.UtcNow;


            var location = await _storageLocationRepository.GetByZoneAndCellAsync(zone, cell);

            await _storageLocationRepository.UpdateAsync(location);

            if (location.IsOccupied) 
                throw new AppException($"Ячейка {zone}-{cell} уже занята другой посылкой.", 409);
            
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
                throw new AppException("Посылка не найдена.", 404);

            if (package.Status != PackageStatus.AwaitingPickup)
            
                throw new AppException($"Нельзя выдать посылку со статусом {package.Status}", 409);
            

            var location = await _storageLocationRepository.GetByIdAsync(package.StorageLocationId.Value);


            package.StorageLocation = location;

            return package;
        }

        public async Task<List<Package>> SearchPackagesByPhone(string phone)
        {
            var client = await _clientRepository.GetByPhoneAsync(phone);

            if (client == null)
                throw new AppException("Клиент с таким номером телефона не найден.", 404);

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
                throw new AppException("Клиент с таким ФИО не найден.", 404);
            


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
                throw new AppException("Посылка не найдена.", 404);


            if (package.Status != PackageStatus.AwaitingPickup && package.Status != PackageStatus.Expired)
                throw new AppException($"Нельзя выдать посылку со статусом {package.Status}", 409);



            var location = await _storageLocationRepository.GetByIdAsync(package.StorageLocationId.Value);

            if (location == null) throw new Exception("Эта посылка не зарегистрирована на складе");

            package.Status = PackageStatus.Issued;

            location.IsOccupied = false;
            
            await _storageLocationRepository.UpdateAsync(location);

            package.StorageLocationId = null;

            await _packageRepository.UpdateAsync(package);

            return package;




        }

        public async Task<bool> ReturnPackageAsync(string number, long returnReasonId, long employeeId)
        {
            var package = await _packageRepository.GetByNumberAsync(number);

            
            if (package == null)
                throw new AppException("Посылка не найдена.", 404);


            if (package.Status != PackageStatus.AwaitingPickup && package.Status != PackageStatus.Expired)
                throw new AppException($"Нельзя вернуть посылку со статусом {package.Status}", 409);



            var location = await _storageLocationRepository.GetByIdAsync(package.StorageLocationId.Value);

            if (location == null) throw new Exception("Эта посылка не зарегистрирована на складе");

            package.Status = PackageStatus.Returned;

            location.IsOccupied = false;

            await _storageLocationRepository.UpdateAsync(location);

            package.StorageLocationId = null;

            //var returnReason = await _returnReasonRepository.GetById(returnReasonId);


            await _packageRepository.UpdateAsync(package);

            var returnRecord = new Return
            {
                PackageId = package.Id,
                EmployeeId = employeeId,
                ReturnReasonId = returnReasonId,
                ReturnDate = DateTime.UtcNow
            };

            await _returnRepository.AddAsync(returnRecord);

            return true;

        }

    

    }
}
