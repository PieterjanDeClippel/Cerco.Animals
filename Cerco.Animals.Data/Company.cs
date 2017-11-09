using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Cerco.Animals.Data;
using Microsoft.AspNetCore.Mvc;

namespace Cerco.Animals.Data
{
    // NuGet package Microsoft.AspNetCore.Mvc

    public class Company
    {
        public int Id { get; set; }

        //[Remote(action: "Verify", controller: "Company", ErrorMessage = "Company name already in use")]
        //[Required(ErrorMessageResourceName = "ValidationRequired", ErrorMessageResourceType = typeof(Errors))]
        //[StringLength(50, MinimumLength = 3, ErrorMessageResourceName = "ValidationStringLength", ErrorMessageResourceType = typeof(Errors))]
        //[UniqueCompany(typeof(Company)]
        public string Name { get; set; }

        //[StringLength(50, MinimumLength = 3, ErrorMessageResourceName = "ValidationStringLength", ErrorMessageResourceType = typeof(Errors))]
        public string Street { get; set; }

        //[Range(1, 10000, ErrorMessageResourceName = "ValidationRange", ErrorMessageResourceType = typeof(Errors))]
        public int? Number { get; set; }

        //[Range(1000, 100000, ErrorMessageResourceName = "ValidationRange", ErrorMessageResourceType = typeof(Errors))]
        public int? PostalCode { get; set; }

        //[StringLength(30, MinimumLength = 2, ErrorMessageResourceName = "ValidationStringLength", ErrorMessageResourceType = typeof(Errors))]
        public string City { get; set; }

        //[StringLength(30, MinimumLength = 2, ErrorMessageResourceName = "ValidationStringLength", ErrorMessageResourceType = typeof(Errors))]
        public string Country { get; set; }

        public virtual List<Animal> Animals { get; set; } = new List<Animal>();
    }
}