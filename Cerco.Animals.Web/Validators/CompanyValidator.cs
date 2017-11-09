using Cerco.Animals.Data;
using Cerco.Animals.Data.Repositories.Interfaces;
using Cerco.Animals.Dto;
using Cerco.Animals.Web.Properties;
using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cerco.Animals.Web.Validators
{
    // https://dzone.com/articles/introduction-to-nodejs-3
    public class CompanyValidator : AbstractValidator<CompanyDto>
    {
        private readonly ICompanyRepository companyRepository;

        // You must pass the context to the constructor, not an entity set
        public CompanyValidator(ICompanyRepository companyRepository)
        {
            this.companyRepository = companyRepository;

            // validation for name
            RuleFor(company => company.Name)
                .NotEmpty().WithLocalizedMessage(typeof(Errors), "ValidationRequired");
            RuleFor(company => company.Name)
                .MinimumLength(3).MaximumLength(50).WithLocalizedMessage(typeof(Errors), "ValidationStringLength");
            RuleFor(company => company.Name)
                .Must(CompanyNameUnique).WithLocalizedMessage(typeof(Errors), "ValidationUnique");

            RuleFor(company => company.Number)
                .InclusiveBetween(0, int.MaxValue).WithLocalizedMessage(typeof(Errors), "ValidationNumber");
            RuleFor(company => company.PostalCode)
                .InclusiveBetween(0, int.MaxValue).WithLocalizedMessage(typeof(Errors), "ValidationNumber");
        }

        /// <summary>Check if there is already a company with the same name</summary>
        /// <param name="edited_company">The company entity that's being updated</param>
        /// <param name="newValue">The new name to be assigned</param>
        /// <returns></returns>
        private bool CompanyNameUnique(CompanyDto edited_company, string newValue)
        {
            // http://www.damirscorner.com/blog/posts/20140519-EnsuringUniquePropertyValueUsingFluentValidation.html

            // Or (1) new name doesn't exist yet
            // or (2) existing name belongs to company being edited
            var result = companyRepository.GetCompanies().All(
                comp => comp.Name != newValue || (comp.Id == edited_company.Id)
            );
            
            return result;
        }
    }
}