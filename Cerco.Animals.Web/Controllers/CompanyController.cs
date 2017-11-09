using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cerco.Animals.Data;
using Cerco.Animals.Web.Models.Company;
using Cerco.Animals.Web.Validators;
using FluentValidation.Results;
using Cerco.Animals.Data.Repositories.Interfaces;
using Cerco.Animals.Data.Repositories;
using Cerco.Animals.Dto;

namespace Cerco.Animals.Web.Controllers
{
    [Route("company")]
    public class CompanyController : Controller
    {
        //private readonly AnimalContext context;
        private ICompanyRepository companyRepository;
        private IAnimalRepository animalRepository;
        public CompanyController(ICompanyRepository companyRepository, IAnimalRepository animalRepository)
        {
            //this.context = context;
            this.companyRepository = companyRepository;
            this.animalRepository = animalRepository;
        }

        [Route("")]
        public IActionResult Index()
        {
            IndexCompanyVM model = new IndexCompanyVM() { Companies = companyRepository.GetCompanies() };
            return View(model);
        }

        [Route("create")]
        public IActionResult Create()
        {
            CreateCompanyVM model = new CreateCompanyVM() { Company = new CompanyDto() };
            return View(model);
        }

        [HttpPost("create")]
        public ActionResult Create(CreateCompanyVM model)
        {
            var validator = new CompanyValidator(companyRepository);
            var result = validator.Validate(model.Company);
            if (result.IsValid)
            {
                var comp = companyRepository.InsertCompany(model.Company);
                companyRepository.SaveChanges();
                return RedirectToAction("Show", "Company", new { comp.Id });
            }
            else
            {
                model.Errors = result.Errors.ToDictionary(e => e.PropertyName, e => string.Format(e.ErrorMessage, e.FormattedMessagePlaceholderValues["PropertyName"]));
                return View(model);
            }
        }

        [Route("{id}")]
        public IActionResult Show(int id)
        {
            var company = companyRepository.GetCompanyById(id);
            //var animals = animalrep
            if(company == null)
            {
                return RedirectToAction("Index", "Company");
            }
            else
            {
                // company.AnimalIds
                ShowCompanyVM model = new ShowCompanyVM() {
                    Company = company,
                    Animals = animalRepository.GetAnimalsForCompany(company)
                };
                return View(model);
            }
        }

        [Route("{id}/edit")]
        public IActionResult Edit(int id)
        {
            var company = companyRepository.GetCompanyById(id);
            if (company == null)
            {
                return RedirectToAction("Index", "Animal");
            }
            else
            {
                EditCompanyVM model = new EditCompanyVM() { Company = company };
                return View(model);
            }
        }
        [HttpPost("{id}/edit")]
        public IActionResult Edit(CompanyDto company)
        {
            var validator = new CompanyValidator(companyRepository);
            var result = validator.Validate(company);
            if (result.IsValid)
            {
                companyRepository.UpdateCompany(company);
                companyRepository.SaveChanges();
                return RedirectToAction("Show", "Company", new { company.Id });
            }
            else
            {
                var errors = result.Errors.ToDictionary(e => e.PropertyName, e => string.Format(e.ErrorMessage, e.FormattedMessagePlaceholderValues["PropertyName"]));
                EditCompanyVM model = new EditCompanyVM() { Company = company, Errors = errors };
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Company");
            }
            //else if (company == null)
            //{
            //    return RedirectToAction("Index", "Company");
            //}
            else
            {
                companyRepository.DeleteCompany(id);
                companyRepository.SaveChanges();
                return RedirectToAction("Index", "Company");
            }
        }
    }
}
