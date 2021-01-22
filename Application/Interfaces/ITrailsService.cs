using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface ITrailsService
    {
        Task<List<Trail>> ListAsync();
        Task<Trail> FindByIdAsync(Guid id);
        Task<bool> AddAsync(Trail trail);
        Task<Trail> FindByNameAsync(string name);
        bool CheckExistsByName(string name);
        bool CheckExistsByAddress(string address);
    }
}