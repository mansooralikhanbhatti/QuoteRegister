using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuoteRegister.Entities;

namespace QuoteTracking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuoteItemController : ControllerBase
    {
        private readonly QuoteTrackingDBContext _context;

        public QuoteItemController(QuoteTrackingDBContext context)
        {
            _context = context;
        }

        // GET
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuoteItem>>> GetQuoteItems()
        {
            return await _context.QuoteItems.Include(qi => qi.Quote).ToListAsync();
        }

        // GET
        [HttpGet("{id}")]
        public async Task<ActionResult<QuoteItem>> GetQuoteItem(int id)
        {
            var quoteItem = await _context.QuoteItems.Include(qi => qi.Quote)
                                                      .FirstOrDefaultAsync(qi => qi.QuoteItemId == id);

            if (quoteItem == null)
            {
                return NotFound();
            }

            return quoteItem;
        }

        // POST
        [HttpPost]
        public async Task<ActionResult<QuoteItem>> PostQuoteItem(QuoteItem quoteItem)
        {
            _context.QuoteItems.Add(quoteItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetQuoteItem), new { id = quoteItem.QuoteItemId }, quoteItem);
        }

        // PUT
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuoteItem(int id, QuoteItem quoteItem)
        {
            if (id != quoteItem.QuoteItemId)
            {
                return BadRequest();
            }

            _context.Entry(quoteItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuoteItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuoteItem(int id)
        {
            var quoteItem = await _context.QuoteItems.FindAsync(id);
            if (quoteItem == null)
            {
                return NotFound();
            }

            _context.QuoteItems.Remove(quoteItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool QuoteItemExists(int id)
        {
            return _context.QuoteItems.Any(e => e.QuoteItemId == id);
        }
    }
}
