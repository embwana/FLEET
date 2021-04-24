using FLEET.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FLEET.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Licence> Licences{ get; set; }
        public DbSet<FLEET.Models.FleetCategory> FleetCategory { get; set; }
        public DbSet<FLEET.Models.COF> COF { get; set; }
        public DbSet<FLEET.Models.Department> Department { get; set; }
        public DbSet<FLEET.Models.FleetCustodian> FleetCustodian { get; set; }
        public DbSet<FLEET.Models.FleetSize> FleetSize { get; set; }
        public DbSet<FLEET.Models.Grounded> Grounded { get; set; }
        public DbSet<FLEET.Models.Insurance> Insurance { get; set; }
        public DbSet<FLEET.Models.InsuranceProvider> InsuranceProvider { get; set; }
        public DbSet<FLEET.Models.Station> Station { get; set; }
        public DbSet<FLEET.Models.Demo> Demo { get; set; }
       // public DbSet<Licence> licences { get; set; }
    }
}
