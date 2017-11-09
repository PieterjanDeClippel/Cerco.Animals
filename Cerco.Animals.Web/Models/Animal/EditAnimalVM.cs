﻿using Cerco.Animals.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cerco.Animals.Web.Models.Animal
{
    public class EditAnimalVM
    {
        public CompanyDto Company { get; set; }
        public AnimalDto Animal { get; set; }
        public Dictionary<string, string> Errors { get; set; } = new Dictionary<string, string>();
    }
}