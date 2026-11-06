using HealthCare_Project.Data;
using HealthCare_Project.Dtos.Appointment;
using HealthCare_Project.Entites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthCare_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AppointmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppointmentReadDto>>> GetAll()
        {
            var appointments = await _context.Appointments
                .Select(a => new AppointmentReadDto
                {
                    PatientId = a.PatientId,
                    DoctorId = a.DoctorId,
                    AppointmentDate = a.AppointmentDate
                })
                .ToListAsync();

            return Ok(appointments);
        }

        [HttpGet("{patientId}/{doctorId}")]
        public async Task<ActionResult<AppointmentReadDto>> GetById(
            int patientId,
            int doctorId)
        {
            var appointment = await _context.Appointments
                .Where(a =>
                    a.PatientId == patientId &&
                    a.DoctorId == doctorId)
                .Select(a => new AppointmentReadDto
                {
                    PatientId = a.PatientId,
                    DoctorId = a.DoctorId,
                    AppointmentDate = a.AppointmentDate
                })
                .FirstOrDefaultAsync();

            if (appointment == null)
                return NotFound();

            return Ok(appointment);
        }

        [HttpPost]
        public async Task<ActionResult<AppointmentReadDto>> Create(
            AppointmentCreateDto dto)
        {
            var patientExists = await _context.Patients
                .AnyAsync(p => p.Id == dto.PatientId);

                return BadRequest("Patient does not exist.");

            var doctorExists = await _context.Doctors
                .AnyAsync(d => d.Id == dto.DoctorId);

            if (!doctorExists)
                return BadRequest("Doctor does not exist.");

            var appointmentExists = await _context.Appointments
                .AnyAsync(a =>
                    a.PatientId == dto.PatientId &&
                    a.DoctorId == dto.DoctorId);

            if (appointmentExists)
                return Conflict("This patient already has an appointment with this doctor.");

            var appointment = new Appointment
            {
                PatientId = dto.PatientId,
                DoctorId = dto.DoctorId,
                AppointmentDate = dto.AppointmentDate
            };

            await _context.Appointments.AddAsync(appointment);

            await _context.SaveChangesAsync();

            var result = new AppointmentReadDto
            {
                PatientId = appointment.PatientId,
                DoctorId = appointment.DoctorId,
                AppointmentDate = appointment.AppointmentDate
            };

            return CreatedAtAction(
                nameof(GetById),
                new
                {
                    patientId = appointment.PatientId,
                    doctorId = appointment.DoctorId
                },
                result
            );
        }

        [HttpPut("{patientId}/{doctorId}")]
        public async Task<IActionResult> Update(
            int patientId,
            int doctorId,
            AppointmentUpdateDto dto)
        {
            var appointment = await _context.Appointments.FindAsync(
                patientId,
                doctorId
            );

            if (appointment == null)
                return NotFound();

            appointment.AppointmentDate = dto.AppointmentDate;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{patientId}/{doctorId}")]
        public async Task<IActionResult> Delete(
            int patientId,
            int doctorId)
        {
            var appointment = await _context.Appointments.FindAsync(
                patientId,
                doctorId
            );

            if (appointment == null)
                return NotFound();

            _context.Appointments.Remove(appointment);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}