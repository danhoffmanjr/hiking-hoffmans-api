using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Application.Errors;
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
            var trail = await context.Trails.Where(x => x.Id == id)
                                            .Include(x => x.Trailhead)
                                            .Include(p => p.Photos)
                                            .Include(e => e.Events)
                                            .FirstOrDefaultAsync();
                                            
            if (trail == null) throw new RestException(HttpStatusCode.NotFound, new { trail = "Not found" });

            return trail;
        }
    }
}