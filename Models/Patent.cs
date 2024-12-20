using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCare.Models;

public class Patient
{
    [Key]
    public int PatientId { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Gender { get; set; }
    public DateTime DOB { get; set; }
    public required string AddressLine1 { get; set; }
    public required string AddressLine2 { get; set; }
    public required string City { get; set; }
    public required string PostCode { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime? ModifiedDate { get; set; }
    public virtual required ICollection<Consultation> Consultations { get; set; }
}


