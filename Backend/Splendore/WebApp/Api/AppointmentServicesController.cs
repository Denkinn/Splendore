using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asp.Versioning;
using BLL.Contracts.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.EF.App;
using Public.DTO.Mappers;
using Public.DTO.v1;

namespace WebApp.Api
{
    /// <inheritdoc />
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AppointmentServicesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IAppBLL _bll;
        private readonly AppointmentServiceMapper _mapper;


        /// <inheritdoc />
        public AppointmentServicesController(ApplicationDbContext context, IAppBLL bll, AutoMapper.IMapper mapper)
        {
            _context = context;
            _bll = bll;
            _mapper = new AppointmentServiceMapper(mapper);
        }

        /// <summary>
        /// Get appointment services. Appointment id could be specified to get all services for specified appointment.
        /// </summary>
        /// <param name="appointmentId">Appointment id.</param>
        /// <returns>List of appointment services.</returns>
        // GET: api/AppointmentServices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Public.DTO.v1.AppointmentService>>> GetAppointmentServices(Guid? appointmentId)
        {
            if (appointmentId != null)
            {
                var appointmentServices = await _bll.AppointmentServiceService.AllByAppointmentIdAsync(appointmentId.Value);
                var appointmentServicesRes = appointmentServices
                    .Select(e => _mapper.Map(e))
                    .ToList();
                return appointmentServicesRes;
            }
            
            var data = await _bll.AppointmentServiceService.AllAsync();

            var res = data
                .Select(e => _mapper.Map(e))
                .ToList();
            
            return res;
        }

        /// <summary>
        /// Get appointment service by id.
        /// </summary>
        /// <param name="id">Id parameter of appointment service.</param>
        /// <returns>Appointment service object.</returns>
        // GET: api/AppointmentServices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Public.DTO.v1.AppointmentService>> GetAppointmentService(Guid id)
        {
            var appointment = await _bll.AppointmentServiceService.FindAsync(id);

            if (appointment == null)
            {
                return NotFound();
            }

            var res = _mapper.Map(appointment);
            
            return res;
        }

        /// <summary>
        /// Update existing appointment service.
        /// </summary>
        /// <param name="id">Id parameter of appointment service to be updated.</param>
        /// <param name="appointmentService">Modified appointment service.</param>
        /// <returns></returns>
        // PUT: api/AppointmentServices/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppointmentService(Guid id, Public.DTO.v1.AppointmentService appointmentService)
        {
            if (id != appointmentService.Id)
            {
                return BadRequest();
            }

            var bllAppointmentService = _mapper.Map(appointmentService);
            _bll.AppointmentServiceService.Update(bllAppointmentService);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Create new appointment service.
        /// </summary>
        /// <param name="appointmentService">Appointment service object to be created.</param>
        /// <returns>Created appointment service.</returns>
        // POST: api/AppointmentServices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Public.DTO.v1.AppointmentService>> PostAppointmentService(Public.DTO.v1.AppointmentService appointmentService)
        {
            var bllAppointmentService = _mapper.Map(appointmentService);
            
            _bll.AppointmentServiceService.Add(bllAppointmentService);

            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetAppointmentService", new { id = appointmentService.Id }, appointmentService);
        }

        /// <summary>
        /// Delete appointment service by id.
        /// </summary>
        /// <param name="id">Id parameter of appointment service to be deleted.</param>
        /// <returns></returns>
        // DELETE: api/AppointmentServices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointmentService(Guid id)
        {
            var appointmentService = await _bll.AppointmentServiceService.RemoveAsync(id);

            if (appointmentService == null) return NotFound();
            
            await _bll.SaveChangesAsync();
            
            return NoContent();
        }
    }
}
