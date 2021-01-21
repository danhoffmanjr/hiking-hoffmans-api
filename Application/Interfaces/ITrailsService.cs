using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface ITrailsService
    {
        Task<List<Trail>> ListAsync();
        Task<Trail> FindByIdAsync(string id);
    }
}