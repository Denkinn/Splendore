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
    public class PaymentMethodsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly PaymentMethodMapper _mapper;

        /// <inheritdoc />
        public PaymentMethodsController(ApplicationDbContext context, AutoMapper.IMapper mapper)
        {
            _context = context;
            _mapper = new PaymentMethodMapper(mapper);
        }

        /// <summary>
        /// Get list of payment methods.
        /// </summary>
        /// <returns>List of payment methods.</returns>
        // GET: api/PaymentMethods
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Public.DTO.v1.PaymentMethod>>> GetPaymentMethods()
        {
            var data = await _context
                .PaymentMethods
                .ToListAsync();
            var res = data
                .Select(e => _mapper.Map(e))
                .ToList();
            return res;
        }

        /// <summary>
        /// Get payment method my id.
        /// </summary>
        /// <param name="id">Id parameter of payment method.</param>
        /// <returns></returns>
        // GET: api/PaymentMethods/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Public.DTO.v1.PaymentMethod>> GetPaymentMethod(Guid id)
        {
            var paymentMethod = await _context.PaymentMethods.FindAsync(id);

            if (paymentMethod == null)
            {
                return NotFound();
            }
            
            var res = _mapper.Map(paymentMethod);

            return res;
        }

        /// <summary>
        /// Update existing payment method.
        /// </summary>
        /// <param name="id">Id parameter of payment method to be updated.</param>
        /// <param name="paymentMethod">Modified payment method.</param>
        /// <returns></returns>
        // PUT: api/PaymentMethods/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaymentMethod(Guid id, Public.DTO.v1.PaymentMethod paymentMethod)
        {
            if (id != paymentMethod.Id)
            {
                return BadRequest();
            }

            var domainPaymentMethod = _mapper.Map(paymentMethod);
            
            _context.Update(_mapper.Map(domainPaymentMethod));

            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Create new payment method.
        /// </summary>
        /// <param name="paymentMethod">Payment method to be created.</param>
        /// <returns>Created payment method.</returns>
        // POST: api/PaymentMethods
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Public.DTO.v1.PaymentMethod>> PostPaymentMethod(Public.DTO.v1.PaymentMethod paymentMethod)
        {
            var domainPaymentMethod = _mapper.Map(paymentMethod);
            
            _context.Add(domainPaymentMethod);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPaymentMethod", new { id = paymentMethod.Id }, paymentMethod);
        }

        /// <summary>
        /// Delete payment method by id.
        /// </summary>
        /// <param name="id">Id parameter of payment method to be deleted.</param>
        /// <returns></returns>
        // DELETE: api/PaymentMethods/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaymentMethod(Guid id)
        {
            if (_context.PaymentMethods == null)
            {
                return NotFound();
            }
            var paymentMethod = await _context.PaymentMethods.FindAsync(id);
            if (paymentMethod == null)
            {
                return NotFound();
            }

            _context.PaymentMethods.Remove(paymentMethod);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
