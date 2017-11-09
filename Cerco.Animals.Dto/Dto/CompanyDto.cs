using System;
using System.Collections.Generic;
using System.Text;

namespace Cerco.Animals.Dto
{
    public class CompanyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public int? Number { get; set; }
        public int? PostalCode { get; set; }
        public String City { get; set; }
        public string Country { get; set; }

        public List<int> AnimalIds { get; set; }
        public List<AnimalDto> Animals { get; set; }
    }
}
