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

            if (trail == null) throw new RestException(HttpStatusCode.NotFound, new { Trail = "Not found" });

            return trail;
        }

        public async Task<Trail> FindByNameAsync(string name)
        {
            var trail = await context.Trails.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower());
            if (trail == null) throw new ArgumentNullException(nameof(trail));
            return trail;
        }

        public bool CheckExistsByName(string name)
        {
            ICollection<string> trailNames = context.Trails.Select(x => x.Name.ToLower()).ToList();
            var exists = trailNames.Any(name => name == name.ToLower());
            return exists;
        }

        public bool CheckExistsByAddress(string address)
        {
            ICollection<string> trailheadLocation = context.Trails.Select(x => x.Trailhead.Address.ToLower()).ToList();
            trailheadLocation.Concat(context.Trails.Select(x => x.Trailhead.InternationalLocation.Address.ToLower()).ToList());
            var exists = trailheadLocation.Any(location => location == address.ToLower());
            return exists;
        }

        public async Task<bool> SaveAsync(Trail trail)
        {
            context.Trails.Add(trail);
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<bool> AddAsync(Trail trail)
        {
            context.Trails.Add(trail);
            return await context.SaveChangesAsync() > 0;
        }
    }
}