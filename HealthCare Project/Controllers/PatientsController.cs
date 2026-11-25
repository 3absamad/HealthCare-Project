using AutoMapper;
using HealthCare_Project.Data;
using HealthCare_Project.Dtos.Appointment;
using HealthCare_Project.Dtos.Patient;
using HealthCare_Project.Entites;
using HealthCare_Project.Results;
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
        private readonly ILogger<PatientsController> _logger;

        public PatientsController(ApplicationDbContext context, IMapper mapper, ILogger<PatientsController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
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
            return Ok(patientResponse);
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
            return Ok(patientResponse);
        }

        #region Pagination and Search
        [HttpGet("paged")]
        public async Task<ActionResult<IEnumerable<PatientReadDto>>> GetAll(int pageNumber, int pageSize, string? searchTerm)
        {
            var totalCount = 0;
            List<Patient> patients = new List<Patient>();
            if (searchTerm is not null)
            {
                 patients = await _context.Patients
                    //.Select(p => new PatientReadDto
                    //{
                    //    Id = p.Id,
                    //    Name = p.Name,
                    //    DateOfBirth = p.DateOfBirth
                    //})
                    .Where(p => p.Name.Contains(searchTerm))
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                totalCount = await _context.Patients
                    .CountAsync(p => p.Name.Contains(searchTerm));
            }

            else
            {
                 patients = await _context.Patients
                                             .Skip((pageNumber - 1) * pageSize)
                                             .Take(pageSize)
                                             .ToListAsync();

                totalCount = await _context.Patients
                                           .CountAsync();
            }

            _logger.LogInformation("Get All Patients Endpoints", patients);

            var patientsResponse = _mapper.Map<List<PatientReadDto>>(patients);
            var paginatedList = new PaginatedList<PatientReadDto>
            {
                PageSize = pageSize,
                PageNumber = pageNumber,
                TotalRecords = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
                Data = patientsResponse
            };

            var result = GenericResult<PaginatedList<PatientReadDto>>.Success(paginatedList);
            return Ok(result);

        }
        #endregion



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
