using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cerco.Animals;
using Cerco.Animals.Dto;

namespace Cerco.Animals.Web.Models.Animal
{
    public class ShowAnimalVM
    {
        public CompanyDto Company { get; set; }
        public AnimalDto Animal { get; set; }
    }
}