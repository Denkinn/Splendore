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
    public class SalonServicesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly SalonServiceMapper _mapper;

        /// <inheritdoc />
        public SalonServicesController(IAppBLL bll, AutoMapper.IMapper mapper)
        {
            _bll = bll;
            _mapper = new SalonServiceMapper(mapper);
        }

        /// <summary>
        /// Get list of salon services. Salon id can be specified to get list of services of this salon.
        /// </summary>
        /// <param name="id">Id parameter of salon (optional).</param>
        /// <returns>List of salon services.</returns>
        // GET: api/SalonServices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Public.DTO.v1.SalonService>>> GetSalonServices(Guid id)
        {
            var data = await _bll.SalonServiceService.AllBySalonIdAsync(id);
            
            var res = data
                .Select(e => _mapper.Map(e))
                .ToList();
            
            return res;
        }

        /// <summary>
        /// Get salon service by id.
        /// </summary>
        /// <param name="id">Id parameter of salon service.</param>
        /// <returns>Salon service.</returns>
        // GET: api/SalonServices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Public.DTO.v1.SalonService>> GetSalonService(Guid id)
        {
            var salonService = await _bll.SalonServiceService.FindAsync(id);

            if (salonService == null)
            {
                return NotFound();
            }

            var res = _mapper.Map(salonService);
            
            return res;
        }

        /// <summary>
        /// Update existing salon service.
        /// </summary>
        /// <param name="id">Id parameter of salon service to be updated.</param>
        /// <param name="salonService">Modified salon service.</param>
        /// <returns></returns>
        // PUT: api/SalonServices/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSalonService(Guid id, Public.DTO.v1.SalonService salonService)
        {
            if (id != salonService.Id)
            {
                return BadRequest();
            }

            var domainSalonService = _mapper.Map(salonService);
            _bll.SalonServiceService.Update(domainSalonService);
            await _bll.SaveChangesAsync();
            
            return NoContent();
        }

        /// <summary>
        /// Create new salon service.
        /// </summary>
        /// <param name="salonService">Salon service to be created.</param>
        /// <returns>Created salon service.</returns>
        // POST: api/SalonServices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Public.DTO.v1.SalonService>> PostSalonService(Public.DTO.v1.SalonService salonService)
        {
            var domainSalon = _mapper.Map(salonService);
            _bll.SalonServiceService.Add(domainSalon);
            
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetSalonService", new { id = salonService.Id }, salonService);
        }

        /// <summary>
        /// Delete salon service by id.
        /// </summary>
        /// <param name="id">Id parameter of salon service to be deleted.</param>
        /// <returns></returns>
        // DELETE: api/SalonServices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalonService(Guid id)
        {
            var salonService = await _bll.SalonServiceService.FindAsync(id);
            if (salonService == null)
            {
                return NotFound();
            }

            _bll.SalonServiceService.Remove(salonService);
            
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
