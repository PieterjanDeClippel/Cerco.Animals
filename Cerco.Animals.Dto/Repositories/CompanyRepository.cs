using Cerco.Animals.Data.Repositories.Interfaces;
using Cerco.Animals.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cerco.Animals.Data.Repositories
{
    public class CompanyRepository : ICompanyRepository, IDisposable
    {
        private AnimalContext context;
        public CompanyRepository(AnimalContext context)
        {
            this.context = context;
        }

        public List<CompanyDto> GetCompanies()
        {
            return context.Companies.Include(comp => comp.Animals)
                .Select(comp => EntityToDto(comp)).ToList();
        }

        public CompanyDto GetCompanyById(int companyId)
        {
            Company company = context.Companies.Include(comp => comp.Animals).SingleOrDefault(comp => comp.Id == companyId);
            return EntityToDto(company);
        }

        public CompanyDto InsertCompany(CompanyDto companyDto)
        {
            Company company = DtoToEntity(companyDto);
            context.Companies.Add(company);
            return EntityToDto(company);
        }

        public void DeleteCompany(int companyId)
        {
            Company company = context.Companies.Find(companyId);
            context.Companies.Remove(company);
        }

        public CompanyDto UpdateCompany(CompanyDto companyDto)
        {
            Company company = context.Companies.Find(companyDto.Id);

            company.Name = companyDto.Name;
            company.Street = companyDto.Street;
            company.Number = companyDto.Number;
            company.PostalCode = companyDto.PostalCode;
            company.City = companyDto.City;
            company.Country = companyDto.Country;

            context.Entry(company).State = EntityState.Modified;

            return EntityToDto(company);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if(!disposed)
            {
                if(disposing)
                {
                    context.Dispose();
                }
            }
            disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public static CompanyDto EntityToDto(Company company)
        {
            return new CompanyDto()
            {
                Id = company.Id,
                Name = company.Name,
                Street = company.Street,
                Number = company.Number,
                PostalCode = company.PostalCode,
                City = company.City,
                Country = company.Country,

                AnimalIds = company.Animals.Select(an => an.Id).ToList(),
                Animals = new List<AnimalDto>()
            };
        }
        public Company DtoToEntity(CompanyDto dto)
        {
            return new Company()
            {
                Id = dto.Id,
                Name = dto.Name,
                Street = dto.Street,
                Number = dto.Number,
                PostalCode = dto.PostalCode,
                City = dto.City,
                Country = dto.Country,

                //Animals = context.Companies.Find(dto.Id).Animals
            };
        }
    }
}