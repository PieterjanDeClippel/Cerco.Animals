using Cerco.Animals.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cerco.Animals.Web.Models.Company
{
    public class ShowCompanyVM
    {
        public CompanyDto Company { get; set; }
        public List<AnimalDto> Animals { get; set; }
    }
}