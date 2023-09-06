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

namespace WebApp.Api
{
    /// <inheritdoc />
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class SchedulesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IAppBLL _bll;
        private readonly ScheduleMapper _mapper;

        /// <inheritdoc />
        public SchedulesController(ApplicationDbContext context, IAppBLL bll, AutoMapper.IMapper mapper)
        {
            _context = context;
            _bll = bll;
            _mapper = new ScheduleMapper(mapper);
        }

        /// <summary>
        /// Get list of schedules. Stylist id can be specified to get list of stylist schedules.
        /// </summary>
        /// <param name="stylistId">Id parameter of stylist (optional).</param>
        /// <returns>List of schedules.</returns>
        // GET: api/Schedules
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Public.DTO.v1.Schedule>>> GetSchedules(Guid? stylistId)
        {
            if (stylistId != null)
            {
                var data1 = await _bll.ScheduleService.AllByStylistIdAsync(stylistId.Value);
                var stylistSchedules = data1
                    .Select(e => _mapper.Map(e))
                    .ToList();
                return stylistSchedules;
            }
            
            var data = await _bll.ScheduleService.AllAsync();

            var res = data
                .Select(e => _mapper.Map(e))
                .ToList();
            
            return res;
        }

        /// <summary>
        /// Get schedule by id.
        /// </summary>
        /// <param name="id">Id parameter of schedule.</param>
        /// <returns>Schedule.</returns>
        // GET: api/Schedules/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Public.DTO.v1.Schedule>> GetSchedule(Guid id)
        {
            var schedule = await _bll.ScheduleService.FindAsync(id);

            if (schedule == null)
            {
                return NotFound();
            }

            var res = _mapper.Map(schedule);
            
            return res;
        }

        /// <summary>
        /// Update existing schedule.
        /// </summary>
        /// <param name="id">Id parameter of schedule.</param>
        /// <param name="schedule">Modified schedule.</param>
        /// <returns></returns>
        // PUT: api/Schedules/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSchedule(Guid id, Public.DTO.v1.Schedule schedule)
        {
            if (id != schedule.Id)
            {
                return BadRequest();
            }

            var bllSchedule = _mapper.Map(schedule);
            _bll.ScheduleService.Update(bllSchedule);
            await _bll.SaveChangesAsync();
            
            return NoContent();
        }

        /// <summary>
        /// Create new schedule.
        /// </summary>
        /// <param name="schedule">Schedule object to be created.</param>
        /// <returns>Created schedule.</returns>
        // POST: api/Schedules
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Public.DTO.v1.Schedule>> PostSchedule(Public.DTO.v1.Schedule schedule)
        {
            var bllSchedule = _mapper.Map(schedule);
            _bll.ScheduleService.Add(bllSchedule);
            
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetSchedule", new { id = schedule.Id }, schedule);
        }

        /// <summary>
        /// Delete schedule by id.
        /// </summary>
        /// <param name="id">Id parameter of schedule.</param>
        /// <returns></returns>
        // DELETE: api/Schedules/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchedule(Guid id)
        {
            var schedule = await _bll.ScheduleService.FindAsync(id);
            if (schedule == null)
            {
                return NotFound();
            }

            _bll.ScheduleService.Remove(schedule);
            
            await _bll.SaveChangesAsync();

            return NoContent();
        }

    }
}