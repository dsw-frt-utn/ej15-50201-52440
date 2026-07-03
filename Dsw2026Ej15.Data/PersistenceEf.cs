namespace Dsw2026Ej15.Data;

using Microsoft.EntityFrameworkCore;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;

public class PersistenceEf : IPersistence
{
    private readonly AppDbContext _context;

    public PersistenceEf(AppDbContext context)
    {
        _context = context;
    }

    public void AddDoctor(Doctor doctor)
    {
        _context.Doctors.Add(doctor);
        _context.SaveChanges();
    }

    public List<Doctor> GetActiveDoctors() =>
        _context.Doctors
            .Where(d => d.IsActive)
            .Include(d => d.Speciality)
            .ToList();

    public Doctor? GetActiveDoctorById(Guid id) =>
        _context.Doctors
            .Include(d => d.Speciality)
            .FirstOrDefault(d => d.Id == id && d.IsActive);

    public bool DeactivateDoctor(Guid id)
    {
        var doctor = _context.Doctors.FirstOrDefault(d => d.Id == id && d.IsActive);
        if (doctor is null) return false;
        doctor.IsActive = false;
        _context.SaveChanges();
        return true;
    }

    public Speciality? GetSpecialityById(Guid id) =>
        _context.Specialities.FirstOrDefault(s => s.Id == id);
}