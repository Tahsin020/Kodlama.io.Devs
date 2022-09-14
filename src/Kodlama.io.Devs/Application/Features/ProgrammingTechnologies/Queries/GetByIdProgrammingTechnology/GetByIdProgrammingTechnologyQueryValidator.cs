using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProgrammingTechnologies.Queries.GetByIdProgrammingTechnology
{
    public class GetByIdProgrammingTechnologyQueryValidator : AbstractValidator<GetByIdProgrammingTechnologyQuery>
    {
        public GetByIdProgrammingTechnologyQueryValidator()
        {
            RuleFor(q => q.Id).NotEmpty();
            RuleFor(q => q.Id).GreaterThan(0);
        }
    }
}
