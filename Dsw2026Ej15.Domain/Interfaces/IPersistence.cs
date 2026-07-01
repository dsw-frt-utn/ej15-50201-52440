namespace Dsw2026Ej15.Domain.Interfaces;

using Dsw2026Ej15.Domain.Entities;

public interface IPersistence
{
    void AddDoctor(Doctor doctor);
    List<Doctor> GetActiveDoctors();
    Doctor? GetActiveDoctorById(Guid id);
    bool DeactivateDoctor(Guid id);
    Speciality? GetSpecialityById(Guid id);
}