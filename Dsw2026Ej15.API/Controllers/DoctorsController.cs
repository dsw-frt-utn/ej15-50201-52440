namespace Dsw2026Ej15.Api.Controllers;

using Dsw2026Ej15.Domain.Exceptions; 
using Dsw2026Ej15.Domain.Interfaces; 
using Dsw2026Ej15.Domain.Entities;   
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/doctors")]
public class DoctorsController : ControllerBase
{
    private readonly IPersistence _persistence;

    public DoctorsController(IPersistence persistence)
    {
        _persistence = persistence;
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateDoctorRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            throw new ValidationException("El nombre es requerido.");
        if (string.IsNullOrWhiteSpace(request.LicenseNumber))
            throw new ValidationException("La matrícula es requerida.");
        var speciality = _persistence.GetSpecialityById(request.SpecialityId);
        if (speciality is null)
            throw new ValidationException("La especialidad no existe.");

        var doctor = new Doctor
        {
            Name = request.Name,
            LicenseNumber = request.LicenseNumber,
            IsActive = true,
            Speciality = speciality
        };
        _persistence.AddDoctor(doctor);
        return StatusCode(201);
    }

    [HttpGet]
    public IActionResult GetAll() => Ok(_persistence.GetActiveDoctors());

    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        var doctor = _persistence.GetActiveDoctorById(id);
        if (doctor is null) return NotFound("Médico no encontrado.");
        return Ok(new { doctor.Name, doctor.LicenseNumber, SpecialityName = doctor.Speciality.Name });
    }

    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        if (!_persistence.DeactivateDoctor(id)) return NotFound("Médico no encontrado.");
        return NoContent();
    }
}

public record CreateDoctorRequest(string Name, string LicenseNumber, Guid SpecialityId);