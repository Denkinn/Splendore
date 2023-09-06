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
using Public.DTO.Mappers;

namespace WebApp.Api
{
    /// <inheritdoc />
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class StylistsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly StylistMapper _mapper;

        /// <inheritdoc />
        public StylistsController(IAppBLL bll, AutoMapper.IMapper mapper)
        {
            _bll = bll;
            _mapper = new StylistMapper(mapper);
        }

        // GET: api/Stylists
        /// <summary>
        /// Ge list of stylists.
        /// </summary>
        /// <returns>Stylist list.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Public.DTO.v1.Stylist>>> GetStylists(Guid? salonId)
        {

            Console.WriteLine(salonId == null);
            if (salonId != null)
            {
                var data1 = await _bll.StylistService.AllBySalonIdAsync(salonId.Value);
                var res1 = data1
                    .Select(e => _mapper.Map(e))
                    .ToList();
                return res1;
            }

            var data = await _bll.StylistService.AllAsync();
            
            var res = data
                .Select(e => _mapper.Map(e))
                .ToList();
            
            return res;
        }

        // GET: api/Stylists/5
        /// <summary>
        /// Find stylist by id.
        /// </summary>
        /// <param name="id">Id parameter of stylist.</param>
        /// <returns>Stylist.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Public.DTO.v1.Stylist>> GetStylist(Guid id)
        {
            var stylist = await _bll.StylistService.FindAsync(id);

            if (stylist == null)
            {
                return NotFound();
            }
            
            var res = _mapper.Map(stylist);

            return res;
        }

        // PUT: api/Stylists/5
        /// <summary>
        /// Update existing stylist.
        /// </summary>
        /// <param name="id">Id parameter of stylist to be updated.</param>
        /// <param name="stylist">Stylist with new parameters.</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStylist(Guid id, Public.DTO.v1.Stylist stylist)
        {
            if (id != stylist.Id)
            {
                return BadRequest();
            }

            var bllStylist = _mapper.Map(stylist);
            _bll.StylistService.Update(bllStylist);
            await _bll.SaveChangesAsync();
            
            return NoContent();
        }

        // POST: api/Stylists
        /// <summary>
        /// Create new stylist.
        /// </summary>
        /// <param name="stylist">Stylist to be created.</param>
        /// <returns>Created stylist.</returns>
        [HttpPost]
        public async Task<ActionResult<Public.DTO.v1.Stylist>> PostStylist(Public.DTO.v1.Stylist stylist)
        {
            var bllStylist = _mapper.Map(stylist);
            _bll.StylistService.Add(bllStylist);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetStylist", new { id = stylist.Id }, stylist);
        }

        // DELETE: api/Stylists/5
        /// <summary>
        /// Delete stylist by id.
        /// </summary>
        /// <param name="id">Id parameter of stylist.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStylist(Guid id)
        {
            var stylist = await _bll.StylistService.FindAsync(id);
            if (stylist == null)
            {
                return NotFound();
            }

            _bll.StylistService.Remove(stylist);
            
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
