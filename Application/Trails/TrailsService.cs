using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Interfaces;
using Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Trails
{
    public class TrailsService : ITrailsService
    {
        private readonly hhDbContext context;
        public TrailsService(hhDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Trail>> ListAsync()
        {
            return await context.Trails.Include(t => t.Trailhead)
                                       .Include(p => p.Photos)
                                       .Include(e => e.Events)
                                       .ToListAsync();
        }
    }
}