using Cerco.Animals.Data.Repositories.Interfaces;
using Cerco.Animals.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cerco.Animals.Data.Repositories
{
    public class AnimalRepository : IAnimalRepository, IDisposable
    {
        private AnimalContext context;
        public AnimalRepository(AnimalContext context)
        {
            this.context = context;
        }

        public List<AnimalDto> GetAnimals()
        {
            //return context.Animals.Include(an => an.Company).ToList();
            return context.Animals.Select(an => new AnimalDto() {
                Id = an.Id,
                Name = an.Name,
                Tattoo = an.Tattoo,
                Gender = an.Gender,
                DateOfBirth = an.DateOfBirth,

                CompanyId = an.Company.Id
            }).ToList();
        }

        public AnimalDto GetAnimalById(int animalId)
        {
            Animal animal = context.Animals.Include(an => an.Company).SingleOrDefault(an => an.Id == animalId);
            return EntityToDto(animal);
        }

        public AnimalDto InsertAnimal(AnimalDto dto)
        {
            Animal animal = DtoToEntity(dto);
            context.Animals.Add(animal);
            return EntityToDto(animal);
        }

        public void DeleteAnimal(int animalId)
        {
            Animal animal = context.Animals.Find(animalId);
            context.Animals.Remove(animal);
        }

        public AnimalDto UpdateAnimal(AnimalDto dto)
        {
            Animal animal = context.Animals.Find(dto.Id);

            animal.Name = dto.Name;
            animal.Tattoo = dto.Tattoo;
            animal.Gender = dto.Gender;
            animal.DateOfBirth = dto.DateOfBirth;

            context.Entry(animal).State = EntityState.Modified;

            return EntityToDto(animal);
        }

        public List<AnimalDto> GetAnimalsForCompany(CompanyDto company)
        {
            return context.Companies.Find(company.Id).Animals.Select(an => new AnimalDto() {
                Id = an.Id,
                Name = an.Name,
                Tattoo = an.Tattoo,
                Gender = an.Gender,
                DateOfBirth = an.DateOfBirth,

                CompanyId = an.Company.Id
            }).ToList();
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if(disposing)
                {
                    context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public static AnimalDto EntityToDto(Animal animal)
        {
            return new AnimalDto()
            {
                Id = animal.Id,
                Name = animal.Name,
                Tattoo = animal.Tattoo,
                Gender = animal.Gender,
                DateOfBirth = animal.DateOfBirth,

                CompanyId = animal.Company.Id
            };
        }

        public Animal DtoToEntity(AnimalDto dto)
        {
            return new Animal()
            {
                Id = dto.Id,
                Name = dto.Name,
                Tattoo = dto.Tattoo,
                Gender = dto.Gender,
                DateOfBirth = dto.DateOfBirth,

                Company = context.Companies.Find(dto.CompanyId)
            };
        }
    }
}