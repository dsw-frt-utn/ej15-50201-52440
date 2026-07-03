namespace Dsw2026Ej15.Data;

using Microsoft.EntityFrameworkCore;
using Dsw2026Ej15.Domain.Entities;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Speciality> Specialities { get; set; }
}