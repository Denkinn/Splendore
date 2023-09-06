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
    public class ReviewsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ReviewMapper _mapper;

        /// <inheritdoc />
        public ReviewsController(IAppBLL bll, AutoMapper.IMapper mapper)
        {
            _bll = bll;
            _mapper = new ReviewMapper(mapper);
        }

        /// <summary>
        /// Get list of reviews.
        /// </summary>
        /// <returns>List of reviews.</returns>
        // GET: api/Reviews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Public.DTO.v1.Review>>> GetReviews()
        {
            var data = await _bll.ReviewService.AllAsync();

            var res = data
                .Select(e => _mapper.Map(e))
                .ToList();
            
            return res;
        }

        /// <summary>
        /// Get review by id.
        /// </summary>
        /// <param name="id">Id parameter of review.</param>
        /// <returns>Review.</returns>
        // GET: api/Reviews/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Public.DTO.v1.Review>> GetReview(Guid id)
        {
            var review = await _bll.ReviewService.FindAsync(id);

            if (review == null)
            {
                return NotFound();
            }

            var res = _mapper.Map(review);
            
            return res;
        }

        /// <summary>
        /// Update existing review.
        /// </summary>
        /// <param name="id">Id parameter of review to be updated.</param>
        /// <param name="review">Modified review.</param>
        /// <returns></returns>
        // PUT: api/Reviews/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReview(Guid id, Public.DTO.v1.Review review)
        {
            if (id != review.Id)
            {
                return BadRequest();
            }

            var bllReview = _mapper.Map(review);
            _bll.ReviewService.Update(bllReview);
            await _bll.SaveChangesAsync();
            
            return NoContent();
        }

        /// <summary>
        /// Create new review.
        /// </summary>
        /// <param name="review">Review to be created.</param>
        /// <returns>Created review.</returns>
        // POST: api/Reviews
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Public.DTO.v1.Review>> PostReview(Public.DTO.v1.Review review)
        {
            var bllReview = _mapper.Map(review);

            var addedReview = await _bll.ReviewService.AddAsync(bllReview);

            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetReview", new { id = addedReview.Id }, addedReview);
        }

        /// <summary>
        /// Delete review by id.
        /// </summary>
        /// <param name="id">Id parameter of review to be deleted.</param>
        /// <returns></returns>
        // DELETE: api/Reviews/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(Guid id)
        {
            var review = await _bll.ReviewService.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }

            _bll.ReviewService.Remove(review);
            
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
