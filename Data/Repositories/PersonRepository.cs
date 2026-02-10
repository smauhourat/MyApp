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
    public class PersonRepository: IRepository<PersonEntity, Guid>, ICodeRepository<PersonEntity>
    {
        private ApplicationDbContext _context;

        public PersonRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PersonEntity?> GetByIdAsync(Guid id)
        {
            return await _context.Persons.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<PersonEntity>> GetAllAsync()
        {
            return await _context.Persons
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task AddAsync(PersonEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            await _context.Persons.AddAsync(entity);
        }

        public Task DeleteAsync(PersonEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            _context.Persons.Remove(entity);
            return Task.CompletedTask;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public Task UpdateAsync(PersonEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            _context.Persons.Update(entity);
            return Task.CompletedTask;
        }

        public async Task<PersonEntity?> GetByCodeAsync(string code)
        {
            ArgumentNullException.ThrowIfNull(code);
            return await _context.Persons.FirstOrDefaultAsync(p => p.Code == code);
        }

        public async Task<bool> ExistsWithCodeAsync(string code)
        {
            ArgumentNullException.ThrowIfNull(code);
            return await _context.Persons.AnyAsync(p => p.Code == code);
        }
    }
}
