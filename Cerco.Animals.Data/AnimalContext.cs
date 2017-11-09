using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cerco.Animals.Data
{
    public class AnimalContext : DbContext
    {
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Company> Companies { get; set; }
        
        public AnimalContext(DbContextOptions options) : base(options)
        {

        }

        protected AnimalContext()
        {

        }
    }
}