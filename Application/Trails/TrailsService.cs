using System;
using System.Collections.Generic;
using System.Linq;
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
                                       .OrderBy(x => x.Name)
                                       .ToListAsync();
        }

        public async Task<Trail> FindByIdAsync(Guid id)
        {
            var trail = await context.Trails.FindAsync(id);
            if (trail == null) throw new ArgumentNullException(nameof(trail));

            return trail;
        }
    }
}