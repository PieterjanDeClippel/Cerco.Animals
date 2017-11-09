using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cerco.Animals.Data;
using Cerco.Animals.Dto;

namespace Cerco.Animals.Web.Models.Company
{
    public class IndexCompanyVM
    {
        public List<CompanyDto> Companies { get; set; }
    }
}