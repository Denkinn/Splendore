using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asp.Versioning;
using BLL.Contracts.App;
using Microsoft.AspNetCore.Mvc;
using DAL.EF.App;
using Helpers.Base;
using Public.DTO.Mappers;

namespace WebApp.Api
{
    /// <inheritdoc />
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class SalonsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly SalonMapper _mapper;

        /// <inheritdoc />
        public SalonsController(IAppBLL bll, AutoMapper.IMapper mapper)
        {
            _bll = bll;
            _mapper = new SalonMapper(mapper);
        }

        // GET: api/Salons
        /// <summary>
        /// Get list of salons.
        /// </summary>
        /// <returns>Salon list.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Public.DTO.v1.Salon>>> GetSalons()
        { 
            var data = await _bll.SalonService.AllAsync();

            var res = data
                .Select(e => _mapper.Map(e))
                .ToList();
            
            return res;
        }

        // GET: api/Salons/5
        /// <summary>
        /// Get salon by id.
        /// </summary>
        /// <param name="id">Id parameter of salon.</param>
        /// <returns>Salon.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Public.DTO.v1.Salon>> GetSalon(Guid id)
        {
            var salon = await _bll.SalonService.FindAsync(id);

            if (salon == null)
            {
                return NotFound();
            }

            var res = _mapper.Map(salon);
            
            return res;
        }

        // PUT: api/Salons/5
        /// <summary>
        /// Update existing salon.
        /// </summary>
        /// <param name="id">Id parameter of salon to be updated.</param>
        /// <param name="salon">Salon with new parameters.</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSalon(Guid id, Public.DTO.v1.Salon salon)
        {
            if (id != salon.Id)
            {
                return BadRequest();
            }

            var bllSalon = _mapper.Map(salon);
            _bll.SalonService.Update(bllSalon);
            await _bll.SaveChangesAsync();
            
            return NoContent();
        }

        // POST: api/Salons
        /// <summary>
        /// Create new salon.
        /// </summary>
        /// <param name="salon">Salon object to be created.</param>
        /// <returns>Created salon object.</returns>
        [HttpPost]
        public async Task<ActionResult<Public.DTO.v1.Salon>> PostSalon(Public.DTO.v1.Salon salon)
        {
            var bllSalon = _mapper.Map(salon);
            _bll.SalonService.Add(bllSalon);
            
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetSalon", new { id = salon.Id }, salon);
        }

        // DELETE: api/Salons/5
        /// <summary>
        /// Delete salon by id.
        /// </summary>
        /// <param name="id">Id parameter of salon to be deleted.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalon(Guid id)
        {
            var salon = await _bll.SalonService.FindAsync(id);
            if (salon == null)
            {
                return NotFound();
            }

            _bll.SalonService.Remove(salon);
            
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
