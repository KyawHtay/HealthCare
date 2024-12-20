using System;
using System.Linq;
using HealthCare.DBContext;
using HealthCare.Models;

public static class DataSeeder
{
    public static void Seed(ABCContext context)
    {
        // Check if there's any data already
        if (!context.Patients.Any())
        {
            // Insert sample patients
            context.Patients.AddRange(
                new Patient
                {
                    FirstName = "John",
                    LastName = "Doe",
                    Gender = "Male",
                    DOB = new DateTime(1980, 5, 20),
                    AddressLine1 = "123 Main St",
                    AddressLine2 = "Apt 4B",
                    City = "London",
                    PostCode = "EC1A 1BB",
                    CreatedDate = DateTime.Now,
                    Consultations = new List<Consultation>() // Initialize Consultations with an empty list
                },
                new Patient
                {
                    FirstName = "Jane",
                    LastName = "Smith",
                    Gender = "Female",
                    DOB = new DateTime(1992, 10, 5),
                    AddressLine1 = "456 Elm Road",
                    AddressLine2 = "Suite 2",
                    City = "Manchester",
                    PostCode = "M1 4AB",
                    CreatedDate = DateTime.Now,
                    Consultations = new List<Consultation>() // Initialize Consultations with an empty list
                },
                 new Patient
                 {
                     FirstName = "Lewis",
                     LastName = "Joghn",
                     Gender = "Male",
                     DOB = new DateTime(1992, 10, 5),
                     AddressLine1 = "111Road",
                     AddressLine2 ="Headpoint 2",
                     City = "Nottigham",
                     PostCode = "NG1 7AB",
                     CreatedDate = DateTime.Now,
                     Consultations = new List<Consultation>() // Initialize Consultations with an empty list
                 }
            );

            context.SaveChanges();
        }

        // You can add similar checks for other entities if needed, 
        // e.g., if (!context.Consultations.Any()) { ... }
    }
}
