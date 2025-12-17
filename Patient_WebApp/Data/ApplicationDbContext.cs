using Microsoft.EntityFrameworkCore;
using Patient_WebApp.Models;
using WebApp_Patient.Models;

namespace WebApp_Patient.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<PatientReference> PatientReferences { get; set; }
        public DbSet<Address> Addresses { get; set; }


    }
}