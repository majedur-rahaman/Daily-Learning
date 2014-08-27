using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CountryCityApp.Models
{
    public class CountryDbContext : DbContext
    {
        public DbSet<Country> Countries { set; get; }
        public DbSet<City> Cities { set; get; }
    }
}