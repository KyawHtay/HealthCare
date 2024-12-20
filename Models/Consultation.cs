using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCare.Models;

public class Consultation
{
    [Key]
    public int ConsultationId { get; set; }
    public int PatientId { get; set; }
    public required Patient Patient { get; set; }
    public DateTime ConsultationDateTime { get; set; }
    public required string PresentingSymptoms { get; set; }
    public required string IndicationsAdvice { get; set; }
    public required string MedicationGiven { get; set; }
    public required string Comments { get; set; }

    [Column("FeeCHarged", TypeName = "decimal(18,2)")]
    public decimal FeeCharged { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime? ModifiedDate { get; set; }
}
