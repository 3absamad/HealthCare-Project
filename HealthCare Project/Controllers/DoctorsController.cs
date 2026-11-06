using HealthCare_Project.Data;
using HealthCare_Project.Dtos.Doctor;
using HealthCare_Project.Entites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthCare_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DoctorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DoctorReadDto>>> GetAll()
        {
            var doctors = await _context.Doctors
                .Select(d => new DoctorReadDto
                {
                    Id = d.Id,
                    Name = d.Name,
                    Specialization = d.Specialization
                })
                .ToListAsync();

            return Ok(doctors);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DoctorReadDto>> GetById(int id)
        {
            var doctor = await _context.Doctors
                .Where(d => d.Id == id)
                .Select(d => new DoctorReadDto
                {
                    Id = d.Id,
                    Name = d.Name,
                    Specialization = d.Specialization
                })
                .FirstOrDefaultAsync();

            if (doctor == null)
                return NotFound();

            return Ok(doctor);
        }

        [HttpPost]
        public async Task<ActionResult<DoctorReadDto>> Create(
            DoctorCreateDto dto)
        {
            var doctor = new Doctor
            {
                Name = dto.Name,
                Specialization = dto.Specialization
            };

            await _context.Doctors.AddAsync(doctor);

            await _context.SaveChangesAsync();

            var result = new DoctorReadDto
            {
                Id = doctor.Id,
                Name = doctor.Name,
                Specialization = doctor.Specialization
            };

            return CreatedAtAction(
                nameof(GetById),
                new { id = doctor.Id },
                result
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            DoctorUpdateDto dto)
        {
            var doctor = await _context.Doctors.FindAsync(id);

            if (doctor == null)
                return NotFound();

            doctor.Name = dto.Name;
            doctor.Specialization = dto.Specialization;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);

            if (doctor == null)
                return NotFound();

            _context.Doctors.Remove(doctor);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}