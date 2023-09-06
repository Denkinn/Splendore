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
    public class ServiceTypesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ServiceTypeMapper _mapper;

        /// <inheritdoc />
        public ServiceTypesController(ApplicationDbContext context, AutoMapper.IMapper mapper)
        {
            _context = context;
            _mapper = new ServiceTypeMapper(mapper);
        }

        // GET: api/ServiceTypes
        /// <summary>
        /// Get list of service types.
        /// </summary>
        /// <returns>Service type list.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Public.DTO.v1.ServiceType>>> GetServiceTypes()
        {
            var data = await _context
                .ServiceTypes
                .ToListAsync();
            var res = data
                .Select(e => _mapper.Map(e))
                .ToList();
            return res;
        }

        // GET: api/ServiceTypes/5
        /// <summary>
        /// Get service type by id.
        /// </summary>
        /// <param name="id">Id parameter of service type to be found.</param>
        /// <returns>Service type.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Public.DTO.v1.ServiceType>> GetServiceType(Guid id)
        {
            var serviceType = await _context.ServiceTypes.FindAsync(id);

            if (serviceType == null)
            {
                return NotFound();
            }
            
            var res = _mapper.Map(serviceType);

            return res;
        }

        // PUT: api/ServiceTypes/5
        /// <summary>
        /// Update existing service type.
        /// </summary>
        /// <param name="id">Id parameter of service type to be updated.</param>
        /// <param name="serviceType">Service type object with changed parameters.</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutServiceType(Guid id, Public.DTO.v1.ServiceType serviceType)
        {
            if (id != serviceType.Id)
            {
                return BadRequest();
            }

            var domainServiceType = _mapper.Map(serviceType);
            
            _context.Update(_mapper.Map(domainServiceType));

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/ServiceTypes
        /// <summary>
        /// Create new service type.
        /// </summary>
        /// <param name="serviceType">Service type to be created.</param>
        /// <returns>Created service type.</returns>
        [HttpPost]
        public async Task<ActionResult<Public.DTO.v1.ServiceType>> PostServiceType(Public.DTO.v1.ServiceType serviceType)
        {
            var domainServiceType = _mapper.Map(serviceType);
            
            _context.Add(domainServiceType);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetServiceType", new { id = serviceType.Id }, serviceType);
        }

        // DELETE: api/ServiceTypes/5
        /// <summary>
        /// Delete service type by id.
        /// </summary>
        /// <param name="id">Id parameter of service type to be deleted.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServiceType(Guid id)
        {
            if (_context.ServiceTypes == null)
            {
                return NotFound();
            }
            var serviceType = await _context.ServiceTypes.FindAsync(id);
            if (serviceType == null)
            {
                return NotFound();
            }

            _context.ServiceTypes.Remove(serviceType);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
