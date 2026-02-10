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
    public class VisitRepository : IVisitRepository<VisitEntity>
    {
        private ApplicationDbContext _context;

        public VisitRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<VisitEntity> GetActiveVisitByPersonCodeAsync(string code)
        {
            var person = await _context.Persons.FirstOrDefaultAsync(p => p.Code == code);
            if (person == null)
            {
                // Throwing here to match the interface contract (non-nullable return type)
                throw new InvalidOperationException($"No person found with code '{code}'.");
            }

            var visit = await _context.Visits.FirstOrDefaultAsync(v => v.PersonId == person.Id && v.Active);
            if (visit == null)
            {
                // Throwing here to match the interface contract (non-nullable return type)
                throw new InvalidOperationException($"No active visit found for person with code '{code}'.");
            }

            return visit;
        }

        public async Task<IEnumerable<VisitEntity>> GetActiveVisitsAsync()
        {
            return await _context.Visits.Where(v => v.Active).ToListAsync();
        }

        public async Task<IEnumerable<VisitEntity>> GetVisitsByPersonIdAsync(Guid personId)
        {
            return await _context.Visits.Where(v => v.PersonId == personId).ToListAsync();
        }

        public async Task<bool> HasActiveVisitAsync(Guid personId)
        {
            return await _context.Visits.AnyAsync(v => v.PersonId == personId && v.Active);
        }
    }
}
