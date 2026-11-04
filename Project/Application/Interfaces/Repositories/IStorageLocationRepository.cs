using Project.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Application.Interfaces.Repositories
{
    public interface IStorageLocationRepository
    {

        Task<List<StorageLocation>> GetFreeAsync();

        Task<StorageLocation?> GetByZoneAndCellAsynс(string zone, string cell);

        Task<StorageLocation?> GetbyIdAsync(long id);

        Task OccupyAsync(long id);

        Task FreeAsync(long id);

 
    }
}
