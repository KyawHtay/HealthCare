using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthCare.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthCare.DBContext
{
    public class ABCContext(DbContextOptions<ABCContext> options) : DbContext(options)
    {
        public required DbSet<Patient> Patients { get; set; }
        public required DbSet<Consultation> Consultations { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           
            
        }

    }
}