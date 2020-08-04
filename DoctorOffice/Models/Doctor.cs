using System.Collections.Generic;

namespace DoctorOffice.Models
{
  public class Doctor
  {
    public Doctor()
    {
      this.Patients = new HashSet<Doctor>Patient();
    }

    public int DoctorId { get; set; }
    public string Name { get; set; }
    public string Specialty { get; set; }
    public virtural ICollection<DoctorPatient> Patients { get; set; }
  }
}