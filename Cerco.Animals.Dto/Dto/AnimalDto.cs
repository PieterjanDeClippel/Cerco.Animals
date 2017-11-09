using Cerco.Animals.Data.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Cerco.Animals.Dto
{
    public class AnimalDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Tattoo { get; set; }
        public Gender Gender { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Date only")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime DateOfBirth { get; set; }


        public int CompanyId { get; set; }
        public CompanyDto Company { get; set; }
    }
}
