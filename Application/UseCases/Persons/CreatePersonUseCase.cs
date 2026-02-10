using Application.DTOs.Persons;
using Domain;
using Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Persons
{
    public class CreatePersonUseCase
    {
        private IRepository<PersonEntity, Guid> _repository;
        private ICodeRepository<PersonEntity> _codeRepository;

        public CreatePersonUseCase(IRepository<PersonEntity, Guid> repository, ICodeRepository<PersonEntity> codeRepository)
        {
            _repository = repository;
            _codeRepository = codeRepository;
        }

        public async Task<PersonEntity> ExecuteAsync(CreatePersonDTO dto)
        {
            var existingPerson = await _codeRepository.GetByCodeAsync(dto.Code);
            if (existingPerson != null)
                throw new InvalidOperationException("A person with the same code already exists.");
            var person = new PersonEntity(dto.Code, dto.FirstName, dto.LastName, dto.Email, dto.PhoneNumber);

            await _repository.AddAsync(person);
            await _repository.SaveChangesAsync();
            return person;
        }
    }
}
