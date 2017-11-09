using Cerco.Animals.Dto;
using Cerco.Animals.Web.Helpers;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cerco.Animals.Web.Models.Company
{
    public class CreateCompanyVM : ISupportErrors
    {
        public CompanyDto Company { get; set; }
        public Dictionary<string, string> Errors { get; set; } = new Dictionary<string, string>();
    }
}