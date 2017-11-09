using Cerco.Animals.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace Cerco.Animals.Data
{
    // NuGet package Microsoft.AspNetCore.Mvc

    public class Animal
    {
        public int Id { get; set; }
        // https://github.com/JeremySkinner/FluentValidation
        
        //[Required(ErrorMessageResourceName = "ValidationRequired", ErrorMessageResourceType = typeof(Errors))]
        public string Name { get; set; }
        public string Tattoo { get; set; }
        public Gender Gender { get; set; }

        //[Required(ErrorMessageResourceName = "ValidationRequired", ErrorMessageResourceType = typeof(Errors))]
        [DataType(DataType.Date, ErrorMessage = "Date only")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime DateOfBirth { get; set; }

        public virtual Company Company { get; set; }
    }
}
