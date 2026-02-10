using Data.Persistence;
using Domain;
using Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class VisitRepository : IRepository<VisitEntity, Guid>, IVisitRepository<VisitEntity>
    {
        private ApplicationDbContext _context;

        public VisitRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<VisitEntity?> GetByIdAsync(Guid id)
        {
            return await _context.Visits
                .Include(v => v.Person) // Include related Person entity if needed
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<IEnumerable<VisitEntity>> GetAllAsync()
        {
            return await _context.Visits
                .Include(v => v.Person) // Include related Person entity if needed
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task AddAsync(VisitEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            await _context.Visits.AddAsync(entity);
        }

        public Task DeleteAsync(VisitEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            _context.Visits.Remove(entity);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(VisitEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            _context.Visits.Update(entity);
            return Task.CompletedTask;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<VisitEntity?> GetActiveVisitByPersonCodeAsync(string code)
        {
            return await _context.Visits
                .Include(v => v.Person) // Include related Person entity to filter by code
                .Where(v => v.Person != null 
                    && v.Person.Code == code 
                    && v.Active)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<VisitEntity>> GetActiveVisitsAsync()
        {
            return await _context.Visits
                .Include(v => v.Person)
                .Where(v => v.Active)
                .ToListAsync();
        }

        public async Task<IEnumerable<VisitEntity>> GetVisitsByPersonIdAsync(Guid personId)
        {
            return await _context.Visits
                .Include(v => v.Person)
                .Where(v => v.PersonId == personId)
                .ToListAsync();
        }

        public async Task<bool> HasActiveVisitAsync(Guid personId)
        {
            return await _context.Visits.AnyAsync(v => v.PersonId == personId && v.Active);
        }
    }
}
