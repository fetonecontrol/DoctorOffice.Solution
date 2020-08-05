using Microsoft.AspNetCore.Mvc;
using DoctorOffice.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DoctorOffice.Controllers
{
  public class DoctorsController : Controller
  {
    private readonly DoctorOfficeContext _db;

    public DoctorsController(DoctorOfficeContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Doctor> model = _db.Doctors.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      ViewBag.SpecialtyId = new SelectList(_db.Specialties, "SpecialtyId", "Name");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Doctor doctor, int SpecialtyId)
    {
      _db.Doctors.Add(doctor);
      if (SpecialtyId != 0)
      {
        _db.DoctorSpecialty.Add(new DoctorSpecialty() { SpecialtyId = SpecialtyId, DoctorId = doctor.DoctorId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisDoctor = _db.Doctors
      .Include(doctor => doctor.Patients)
      .ThenInclude(join => join.Patient)
      .Include(doctor => doctor.Specialties)
      .ThenInclude(join => join.Specialty)
      .FirstOrDefault(doctor => doctor.DoctorId == id);
      return View(thisDoctor);
    }

    public ActionResult Edit(int id)
    {
      ViewBag.SpecialtyId = new SelectList(_db.Specialties, "SpecialtyId", "Name");
      var thisDoctor = _db.Doctors.FirstOrDefault(doctor => doctor.DoctorId == id);
      return View(thisDoctor);
    }

    [HttpPost]
    public ActionResult Edit(Doctor doctor, int SpecialtyId)
    {
      if (SpecialtyId != 0)
      {
        _db.DoctorSpecialty.Add(new DoctorSpecialty() { SpecialtyId = SpecialtyId, DoctorId = doctor.DoctorId });
      }
      _db.Entry(doctor).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisDoctor = _db.Doctors.FirstOrDefault(doctor => doctor.DoctorId == id);
      return View(thisDoctor);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisDoctor = _db.Doctors.FirstOrDefault(doctor => doctor.DoctorId == id);
      _db.Doctors.Remove(thisDoctor);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}