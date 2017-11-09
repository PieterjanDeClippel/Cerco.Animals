using Cerco.Animals.Data;
using Cerco.Animals.Data.Repositories.Interfaces;
using Cerco.Animals.Web.Properties;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cerco.Animals.Data.Enums;
using Cerco.Animals.Dto;

namespace Cerco.Animals.Web.Validators
{
    public class AnimalValidator : AbstractValidator<AnimalDto>
    {
        //private readonly AnimalContext context;
        private readonly ICompanyRepository companyRepository;
        private readonly IAnimalRepository animalRepository;

        public AnimalValidator(ICompanyRepository companyRepository, IAnimalRepository animalRepository)
        {
            this.companyRepository = companyRepository;
            this.animalRepository = animalRepository;

            // validation for name
            RuleFor(animal => animal.Name)
                .NotEmpty().WithLocalizedMessage(typeof(Errors), "ValidationRequired");
            RuleFor(animal => animal.Name)
                .MaximumLength(50).WithLocalizedMessage(typeof(Errors), "ValidationStringLength");
            RuleFor(animal => animal.Name)
                .Must(AnimalNameUniqueForCompany).WithLocalizedMessage(typeof(Errors), "ValidationUnique");

            // validation for tattoo
            RuleFor(animal => animal.Tattoo)
                .Must(AnimalTattooUniqueForCompany).WithLocalizedMessage(typeof(Errors), "ValidationUnique");

            // validation for gender (extra security)
            RuleFor(animal => animal.Gender)
                .Must(AnimalGenderDefined);

            // validation for birth date
            RuleFor(animal => animal.DateOfBirth)
                .NotEmpty();
        }

        private bool AnimalGenderDefined(Gender gender)
        {
            return new Gender[] { Gender.Male, Gender.Female }.Contains(gender);
        }

        private bool AnimalNameUniqueForCompany(AnimalDto edited_animal, string newValue)
        {
            var company = companyRepository.GetCompanyById(edited_animal.CompanyId);
            var animals = animalRepository.GetAnimalsForCompany(company);

            var result = animals.All(
                an => an.Name != newValue || (an.Id == edited_animal.Id)
            );

            return result;
        }
        private bool AnimalTattooUniqueForCompany(AnimalDto edited_animal, string newValue)
        {
            var company = companyRepository.GetCompanyById(edited_animal.CompanyId);
            var animals = animalRepository.GetAnimalsForCompany(company);

            var result = animals.All(
                an => an.Tattoo != newValue || (an.Id == edited_animal.Id)
            );

            return result;
        }

    }
}
