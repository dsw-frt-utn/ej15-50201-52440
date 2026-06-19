namespace Dsw2026Ej15.Data;

using System.Text.Json;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;

public class PersistenceInMemory : IPersistence
{
    private readonly List<Doctor> _doctors = new();
    private readonly List<Speciality> _specialities = new();

    public PersistenceInMemory()
    {
        var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "specialities.json");
        var json = File.ReadAllText(path);
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var list = JsonSerializer.Deserialize<List<Speciality>>(json, options);
        if (list != null) _specialities.AddRange(list);
    }

    public void AddDoctor(Doctor doctor) => _doctors.Add(doctor);
    public List<Doctor> GetActiveDoctors() => _doctors.Where(d => d.IsActive).ToList();
    public Doctor? GetActiveDoctorById(Guid id) => _doctors.FirstOrDefault(d => d.Id == id && d.IsActive);
    public bool DeactivateDoctor(Guid id)
    {
        var doctor = _doctors.FirstOrDefault(d => d.Id == id && d.IsActive);
        if (doctor is null) return false;
        doctor.IsActive = false;
        return true;
    }
    public Speciality? GetSpecialityById(Guid id) => _specialities.FirstOrDefault(s => s.Id == id);
}