using Cerco.Animals.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cerco.Animals.Data.Repositories.Interfaces
{
    public interface IAnimalRepository : IDisposable
    {
        List<AnimalDto> GetAnimals();
        AnimalDto GetAnimalById(int animalId);
        AnimalDto InsertAnimal(AnimalDto animal);
        void DeleteAnimal(int animalId);
        AnimalDto UpdateAnimal(AnimalDto animal);
        List<AnimalDto> GetAnimalsForCompany(CompanyDto company);
        void SaveChanges();
    }
}