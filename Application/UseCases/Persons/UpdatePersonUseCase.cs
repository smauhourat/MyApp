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
    public class UpdatePersonUseCase
    {
        private IRepository<PersonEntity, Guid> _repository;

        public UpdatePersonUseCase(IRepository<PersonEntity, Guid> repository)            
        {
            _repository = repository;
        }

        public async Task<PersonEntity> ExecuteAsync(UpdatePersonDTO dto)
        {
            var person = await _repository.GetByIdAsync(dto.Id);
            if (person == null)
                throw new InvalidOperationException("Person not found.");

            person.UpdatePersonalInfo(dto.FirstName, dto.LastName, dto.Email, dto.PhoneNumber);
            await _repository.UpdateAsync(person);
            await _repository.SaveChangesAsync();
            return person;
        }
    }
}
