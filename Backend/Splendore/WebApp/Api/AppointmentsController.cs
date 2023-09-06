using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asp.Versioning;
using BLL.Contracts.App;
using DAL.Contracts.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.EF.App;
using Helpers.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Public.DTO.Mappers;

namespace WebApp.Api
{
    /// <inheritdoc />
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly AppointmentMapper _mapper;

        /// <inheritdoc />
        public AppointmentsController(IAppBLL bll, AutoMapper.IMapper mapper)
        {
            _bll = bll;
            _mapper = new AppointmentMapper(mapper);
        }

        /// <summary>
        /// Get list of appointments.
        /// </summary>
        /// <returns>Appointment list.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Public.DTO.v1.Appointment>>> GetAppointments()
        {
            var data = await _bll.AppointmentService.AllAsync(User.GetUserId());

            var res = data
                .Select(e => _mapper.Map(e))
                .ToList();
            
            return res;
        }

        // GET: api/Appointments/5
        /// <summary>
        /// Get appointment by id.
        /// </summary>
        /// <param name="id">Id parameter of appointment.</param>
        /// <returns>Appointment.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Public.DTO.v1.Appointment>> GetAppointment(Guid id)
        {
            var appointment = await _bll.AppointmentService.FindAsync(id, User.GetUserId());

            if (appointment == null)
            {
                return NotFound();
            }

            var res = _mapper.Map(appointment);
            
            return res;
        }

        // PUT: api/Appointments/5
        /// <summary>
        /// Update appointment by id.
        /// </summary>
        /// <param name="id">Id parameter of appointment to change.</param>
        /// <param name="appointment">Appointment with changes.</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppointment(Guid id, Public.DTO.v1.Appointment appointment)
        {
            if (id != appointment.Id)
            {
                return BadRequest();
            }

            var bllAppointment = _mapper.Map(appointment);
            _bll.AppointmentService.Update(bllAppointment);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Appointments
        /// <summary>
        /// Create new appointment.
        /// </summary>
        /// <param name="appointment">Appointment.</param>
        /// <returns>Created appointment.</returns>
        [HttpPost]
        public async Task<ActionResult<Public.DTO.v1.Appointment>> PostAppointment(Public.DTO.v1.Appointment appointment)
        {
            var bllAppointment = _mapper.Map(appointment);
            
            bllAppointment!.AppUserId = User.GetUserId();

            var addedAppointment = await _bll.AppointmentService.AddAsync(bllAppointment);

            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetAppointment", new { id = addedAppointment.Id }, addedAppointment);
        }

        // DELETE: api/Appointments/5
        /// <summary>
        /// Delete appointment by id.
        /// </summary>
        /// <param name="id">Id parameter of appointment to delete.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(Guid id)
        {
            var appointment = await _bll.AppointmentService.RemoveAsync(id, User.GetUserId());

            if (appointment == null) return NotFound();
            
            await _bll.SaveChangesAsync();
            
            return NoContent();
        }
    }
}
