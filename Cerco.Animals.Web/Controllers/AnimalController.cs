using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cerco.Animals.Data;
using Cerco.Animals.Web.Models.Animal;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Cerco.Animals.Web.Validators;
using Cerco.Animals.Data.Repositories;
using Cerco.Animals.Data.Repositories.Interfaces;
using Cerco.Animals.Dto;

namespace Cerco.Animals.Web.Controllers
{
    [Route("company/{company_id}/animals")]
    public class AnimalController : Controller
    {
        //private readonly AnimalContext context;
        private ICompanyRepository companyRepository;
        private IAnimalRepository animalRepository;
        public AnimalController(ICompanyRepository companyRepository, IAnimalRepository animalRepository)
        {
            this.companyRepository = companyRepository;
            this.animalRepository = animalRepository;
        }

        [Route("")]
        public IActionResult Index(int company_id)
        {
            var company = companyRepository.GetCompanyById(company_id);

            IndexAnimalVM model = new IndexAnimalVM() {
                Company = company,
                Animals = company.Animals == null ? new List<AnimalDto>() : company.Animals.ToList()
            };
            return View(model);
        }

        [Route("create")]
        public IActionResult Create(int company_id)
        {
            var company = companyRepository.GetCompanyById(company_id);
            if (company == null)
            {
                return RedirectToAction("Index", "Company");
            }
            else
            {
                CreateAnimalVM model = new CreateAnimalVM()
                {
                    Company = company,
                    Animal = new AnimalDto()
                    {
                        Gender = Data.Enums.Gender.Female,
                        DateOfBirth = new DateTime(1990, 1, 1),
                        Company = company
                    }
                };
                return View(model);
            }
        }

        [HttpPost("create")]
        public IActionResult Create(int company_id, CreateAnimalVM model)
        {
            model.Animal.CompanyId = company_id;

            var validator = new AnimalValidator(companyRepository, animalRepository);
            var result = validator.Validate(model.Animal);
            if (result.IsValid)
            {
                var anim = animalRepository.InsertAnimal(model.Animal);
                animalRepository.SaveChanges();
                return RedirectToAction("Show", "Animal", new { anim.Id });
            }
            else
            {
                // validation failed
                model.Errors = result.Errors.ToDictionary(e => e.PropertyName, e => string.Format(e.ErrorMessage, e.FormattedMessagePlaceholderValues["PropertyName"]));

                model.Company = model.Animal.Company = companyRepository.GetCompanyById(model.Animal.CompanyId);

                return View(model);
            }
        }

        [Route("{id}")]
        public IActionResult Show(int company_id, int id)
        {
            var company = companyRepository.GetCompanyById(company_id);
            var animal = animalRepository.GetAnimalById(id);
            if (animal == null)
            {
                return RedirectToAction("Index", "Animal");
            }
            else
            {
                ShowAnimalVM model = new ShowAnimalVM() { Animal = animal, Company = company };
                return View(model);
            }
        }

        [Route("{id}/edit")]
        public IActionResult Edit(int company_id, int id)
        {
            var company = companyRepository.GetCompanyById(company_id);
            var animal = animalRepository.GetAnimalById(id);
            if (animal == null)
            {
                return RedirectToAction("Index", "Animal");
            }
            else
            {
                EditAnimalVM model = new EditAnimalVM() { Animal = animal, Company = company };
                return View(model);
            }
        }
        [HttpPost("{id}/edit")]
        public IActionResult Edit(int company_id, EditAnimalVM model)
        {
            model.Company = companyRepository.GetCompanyById(company_id);
            
            var validator = new AnimalValidator(companyRepository, animalRepository);
            var result = validator.Validate(model.Animal);
            if (result.IsValid)
            {
                animalRepository.UpdateAnimal(model.Animal);
                animalRepository.SaveChanges();
                return RedirectToAction("Show", "Animal", new { model.Animal.Id });
            }
            else
            {
                model.Errors = result.Errors.ToDictionary(e => e.PropertyName, e => string.Format(e.ErrorMessage, e.FormattedMessagePlaceholderValues["PropertyName"]));
                return View(model);
            }
        }

        [HttpPost("{id}/delete")]
        public ActionResult Delete(int company_id, int id)
        {
            var animal = animalRepository.GetAnimalById(id);
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Show", "Company", new { id = company_id });
            }
            else if(animal == null)
            {
                return RedirectToAction("Show", "Company", new { id = company_id });
            }
            else
            {
                animalRepository.DeleteAnimal(animal.Id);
                animalRepository.SaveChanges();
                return RedirectToAction("Show", "Company", new { id = company_id });
            }
        }
    }
}
