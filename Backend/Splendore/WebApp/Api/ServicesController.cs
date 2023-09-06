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
    public class ServicesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IAppBLL _bll;
        private readonly ServiceMapper _mapper;

        /// <inheritdoc />
        public ServicesController(ApplicationDbContext context, IAppBLL bll, AutoMapper.IMapper mapper)
        {
            _context = context;
            _bll = bll;
            _mapper = new ServiceMapper(mapper);
        }

        // GET: api/Services
        /// <summary>
        /// Get list of services.
        /// </summary>
        /// <returns>Service list.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Public.DTO.v1.Service>>> GetServices()
        {
            var data = await _bll.ServiceService.AllAsync();

            var res = data
                .Select(e => _mapper.Map(e))
                .ToList();
            
            return res;
        }

        // GET: api/Services/5
        /// <summary>
        /// Get service by id.
        /// </summary>
        /// <param name="id">Id parameter of service.</param>
        /// <returns>Service.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Public.DTO.v1.Service>> GetService(Guid id)
        {
            var service = await _bll.ServiceService.FindAsync(id);

            if (service == null)
            {
                return NotFound();
            }

            var res = _mapper.Map(service);
            
            return res;
        }

        // PUT: api/Services/5
        /// <summary>
        /// Update existing service.
        /// </summary>
        /// <param name="id">Id parameter of service.</param>
        /// <param name="service">Service with changed parameters.</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutService(Guid id, Public.DTO.v1.Service service)
        {
            if (id != service.Id)
            {
                return BadRequest();
            }

            var bllService = _mapper.Map(service);
            _bll.ServiceService.Update(bllService);
            await _bll.SaveChangesAsync();
            
            return NoContent();
        }

        // POST: api/Services
        /// <summary>
        /// Create new service.
        /// </summary>
        /// <param name="service">Service to be created.</param>
        /// <returns>Created service.</returns>
        [HttpPost]
        public async Task<ActionResult<Public.DTO.v1.Service>> PostService(Public.DTO.v1.Service service)
        {
            var bllService = _mapper.Map(service);
            _bll.ServiceService.Add(bllService);
            
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetService", new { id = service.Id }, service);
        }

        // DELETE: api/Services/5
        /// <summary>
        /// Delete service by id.
        /// </summary>
        /// <param name="id">Id parameter of service.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteService(Guid id)
        {
            var service = await _bll.ServiceService.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }

            _bll.ServiceService.Remove(service);
            
            await _bll.SaveChangesAsync();

            return NoContent();
        }
        
    }
}
