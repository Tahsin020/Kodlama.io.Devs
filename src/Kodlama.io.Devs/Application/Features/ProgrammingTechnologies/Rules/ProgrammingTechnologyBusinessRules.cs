using Application.Features.ProgrammingTechnologies.Constants;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProgrammingTechnologies.Rules
{
    public class ProgrammingTechnologyBusinessRules
    {
        private readonly IProgrammingTechnologyRepository _programmingTechnologyRepository;

        public ProgrammingTechnologyBusinessRules(IProgrammingTechnologyRepository programmingTechnologyRepository)
        {
            _programmingTechnologyRepository = programmingTechnologyRepository;
        }

        public async Task TechnologyNameCanNotBeDuplicatedWhenInserted(string name)
        {
            IPaginate<ProgrammingTechnology> result = await _programmingTechnologyRepository.GetListAsync(p => p.Name == name);
            if (result.Items.Any())
                throw new BusinessException(Messages.TechnologyNameExists);
        }

        public async Task TechnologyNameCanNotBeDuplicatedWhenUpdated(int id, string name)
        {
            IPaginate<ProgrammingTechnology> result = await _programmingTechnologyRepository.GetListAsync(p => p.Id != id && p.Name == name);
            if (result.Items.Any())
                throw new BusinessException(Messages.TechnologyNameExists);
        }

        public void TechnologyShouldExistWhenRequested(ProgrammingTechnology programmingTechnology)
        {
            if (programmingTechnology is null)
                throw new BusinessException(Messages.RequestedTechnologyDoesNotExist);
        }
    }
}
