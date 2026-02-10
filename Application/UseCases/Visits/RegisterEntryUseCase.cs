using Application.DTOs.Visits;
using Domain;
using Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Visits
{
    public class RegisterEntryUseCase
    {
        private readonly IRepository<VisitEntity, Guid> _repository;
        private readonly IVisitRepository<VisitEntity> _visitRepository;
        private readonly ICodeRepository<PersonEntity> _personCodeRepository;
        public RegisterEntryUseCase(IRepository<VisitEntity, Guid> repository, IVisitRepository<VisitEntity> visitRepository, ICodeRepository<PersonEntity> personCodeRepository)
        {
            _repository = repository;
            _visitRepository = visitRepository;
            _personCodeRepository = personCodeRepository;
        }

        public async Task<VisitEntity> ExecuteAsync(RegisterEntryDTO dto)
        {
            Guid personId;
            if (dto.PersonId != Guid.Empty)
            {
                personId = dto.PersonId;
            }
            else if (!string.IsNullOrWhiteSpace(dto.Code))
            {
                var person = await _personCodeRepository.GetByCodeAsync(dto.Code);
                if (person == null)
                {
                    throw new InvalidOperationException($"No person found with code '{dto.Code}'.");
                }
                personId = person.Id;
            }
            else 
            { 
                throw new ArgumentException("Either PersonId or Code must be provided to register an entry.");
            }

            if (await _visitRepository.HasActiveVisitAsync(personId))
            {
                throw new InvalidOperationException($"Person with ID '{personId}' already has an active visit.");
            }

            var visit = new VisitEntity(personId, dto.EntryTime);

            await _repository.AddAsync(visit);
            await _repository.SaveChangesAsync();

            return await _repository.GetByIdAsync(visit.Id) ?? throw new InvalidOperationException($"Failed to retrieve the newly created visit with ID '{visit.Id}'.");
        }
    }
}
