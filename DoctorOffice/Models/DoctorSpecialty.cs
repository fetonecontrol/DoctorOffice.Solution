namespace DoctorOffice.Models
{
  public class DoctorPatient
  {
    public int DoctorSpecialtyId { get; set; }
    public int DoctorId { get; set; }
    public int SpecialtyId { get; set; }
    public Doctor Doctor { get; set; }
    public Specialty Specialty { get; set; }
  }
}