using AutoMapper;
using HealthCare_Project.Data;
using HealthCare_Project.Dtos.Appointment;
using HealthCare_Project.Dtos.Patient;
using HealthCare_Project.Entites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace HealthCare_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PatientsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientReadDto>>> GetAll()
        {
            var patients = await _context.Patients
                //.Select(p => new PatientReadDto
                //{
                //    Id = p.Id,
                //    Name = p.Name,
                //    DateOfBirth = p.DateOfBirth
                //})
                .ToListAsync();

            var patientResponse = _mapper.Map<List<Patient>?, List<PatientReadDto>>(patients);
            return Ok(patients);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PatientReadDto>> GetById(int id)
        {
            var patient = await _context.Patients
                //.Where(p => p.Id == id)
                //.Select(p => new PatientReadDto
                //{
                //    Id = p.Id,
                //    Name = p.Name,
                //    DateOfBirth = p.DateOfBirth
                //})
                .FirstOrDefaultAsync(p => p.Id == id);

            if (patient == null)
                return NotFound();

            var patientResponse = _mapper.Map<Patient, PatientReadDto>(patient);
            return Ok(patient);
        }

        [HttpPost]
        public async Task<ActionResult<PatientReadDto>> Create(
            PatientCreateDto dto)
        {
            var patient = new Patient
            {
                Name = dto.Name,
                DateOfBirth = dto.DateOfBirth
            };

            await _context.Patients.AddAsync(patient);

            await _context.SaveChangesAsync();

            var result = new PatientReadDto
            {
                Id = patient.Id,
                Name = patient.Name,
                DateOfBirth = patient.DateOfBirth
            };

            return CreatedAtAction(
                nameof(GetById),
                new { id = patient.Id },
                result
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            PatientUpdateDto dto)
        {
            var patient = await _context.Patients.FindAsync(id);

            if (patient == null)
                return NotFound();

            patient.Name = dto.Name;
            patient.DateOfBirth = dto.DateOfBirth;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var patient = await _context.Patients.FindAsync(id);

            if (patient == null)
                return NotFound();

            _context.Patients.Remove(patient);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
