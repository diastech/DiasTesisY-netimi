using DIAS.Domain.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIAS.Application.Validation
{

    //TODO LATER
    // Validator
    public class ParameterValidator : AbstractValidator<AssigmentGroup>
    {
        public ParameterValidator()
        {
            RuleFor(x => x.asgGroupName).NotNull().NotEmpty().WithMessage("Please specify a Parameter Name.");

            //RuleFor(x => x.Postcode).NotNull();
            //RuleFor(x => x.Postcode).Must(BeAValidPostcode).WithMessage("Please specify a valid postcode");
        }

        //private bool BeAValidPostcode(string postcode)
        //{
        //    // custom postcode validating logic goes here.

        //    return true;
        //}
    }
}
