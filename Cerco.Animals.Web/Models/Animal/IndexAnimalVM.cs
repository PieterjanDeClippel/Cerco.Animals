using Cerco.Animals.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cerco.Animals.Web.Models.Animal
{
    public class IndexAnimalVM
    {
        public CompanyDto Company { get; set; }
        public List<AnimalDto> Animals { get; set; }
    }
}