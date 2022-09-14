using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProgrammingTechnologies.Commands.UpdateProgrammingTechnology
{
    public class UpdateProgrammingTechnologyCommandValidator : AbstractValidator<ProgrammingTechnology>
    {
        public UpdateProgrammingTechnologyCommandValidator()
        {
            RuleFor(c => c.Id).NotEmpty();
            RuleFor(c => c.Id).GreaterThan(0);
            RuleFor(c => c.ProgrammingLanguageId).NotEmpty();
            RuleFor(c => c.ProgrammingLanguageId).GreaterThan(0);
            RuleFor(c => c.Name).NotEmpty();
        }

    }
}
