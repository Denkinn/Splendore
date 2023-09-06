using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.EF.App;
using Public.DTO.Mappers;

namespace WebApp.Api
{
    /// <inheritdoc />
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AppointmentStatusesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly AppointmentStatusMapper _mapper;

        /// <inheritdoc />
        public AppointmentStatusesController(ApplicationDbContext context, AutoMapper.IMapper mapper)
        {
            _context = context;
            _mapper = new AppointmentStatusMapper(mapper);
        }

        /// <summary>
        /// Get list of appointment statuses. Status name can be specified to get list of statuses by this name.
        /// </summary>
        /// <param name="statusName">Status name in string format.</param>
        /// <returns>List of appointment statuses.</returns>
        // GET: api/AppointmentStatuses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Public.DTO.v1.AppointmentStatus>>> GetAppointmentStatuses(string? statusName)
        {
            if (statusName != null) 
            {
                var data1 = await _context
                    .AppointmentStatuses
                    .Where(s => s.Name == statusName)
                    .ToListAsync();
                var res1 = data1
                    .Select(e => _mapper.Map(e))
                    .ToList();
                return res1;
            }
            
            var data = await _context
                .AppointmentStatuses
                .ToListAsync();
            var res = data
                .Select(e => _mapper.Map(e))
                .ToList();
            return res;
            
        }

        /// <summary>
        /// Get appointment status by id.
        /// </summary>
        /// <param name="id">Id parameter of appointment status.</param>
        /// <returns>Appointment status.</returns>
        // GET: api/AppointmentStatuses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Public.DTO.v1.AppointmentStatus>> GetAppointmentStatus(Guid id)
        {
            var appointmentStatus = await _context.AppointmentStatuses.FindAsync(id);

            if (appointmentStatus == null)
            {
                return NotFound();
            }
            
            var res = _mapper.Map(appointmentStatus);

            return res;
        }

        /// <summary>
        /// Update existing appointment status.
        /// </summary>
        /// <param name="id">Id parameter of appointment status to be updated.</param>
        /// <param name="appointmentStatus">Modified appointment status object.</param>
        /// <returns></returns>
        // PUT: api/AppointmentStatuses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppointmentStatus(Guid id, Public.DTO.v1.AppointmentStatus appointmentStatus)
        {
            if (id != appointmentStatus.Id)
            {
                return BadRequest();
            }

            var domainAppointmentStatus = _mapper.Map(appointmentStatus);
            
            _context.Update(_mapper.Map(domainAppointmentStatus));

            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Create new appointment status.
        /// </summary>
        /// <param name="appointmentStatus">Appointment status object to be created.</param>
        /// <returns>Created appointment status.</returns>
        // POST: api/AppointmentStatuses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Public.DTO.v1.AppointmentStatus>> PostAppointmentStatus(Public.DTO.v1.AppointmentStatus appointmentStatus)
        {
            var domainAppointmentStatus = _mapper.Map(appointmentStatus);
            
            _context.Add(domainAppointmentStatus);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAppointmentStatus", new { id = appointmentStatus.Id }, appointmentStatus);
        }

        /// <summary>
        /// Delete appointment status by id.
        /// </summary>
        /// <param name="id">Id parameter of appointment status to be deleted.</param>
        /// <returns></returns>
        // DELETE: api/AppointmentStatuses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointmentStatus(Guid id)
        {
            var appointmentStatus = await _context.AppointmentStatuses.FindAsync(id);
            if (appointmentStatus == null)
            {
                return NotFound();
            }

            _context.AppointmentStatuses.Remove(appointmentStatus);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        
    }
}
