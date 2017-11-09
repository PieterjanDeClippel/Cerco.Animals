using Cerco.Animals.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cerco.Animals.Data.Repositories.Interfaces
{
    public interface ICompanyRepository : IDisposable
    {
        List<CompanyDto> GetCompanies();
        CompanyDto GetCompanyById(int companyId);
        CompanyDto InsertCompany(CompanyDto company);
        void DeleteCompany(int companyId);
        CompanyDto UpdateCompany(CompanyDto company);
        void SaveChanges();
    }
}