using Domain;
using Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Visits
{
    public class GetActiveVisitsUseCase
    {
        private IVisitRepository<VisitEntity> _visitRepository;
        public GetActiveVisitsUseCase(IVisitRepository<VisitEntity> visitRepository)
        {
            _visitRepository = visitRepository;
        }
        public async Task<IEnumerable<VisitEntity>> ExecuteAsync()
        {
            return await _visitRepository.GetActiveVisitsAsync();
        }
    }
}
