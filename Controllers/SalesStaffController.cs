using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuoteRegister.Entities;

namespace QuoteTracking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesStaffController : ControllerBase
    {
        private readonly QuoteTrackingDBContext _context;

        public SalesStaffController(QuoteTrackingDBContext context)
        {
            _context = context;
        }

        // GET
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalesStaff>>> GetSalesStaff()
        {
            return await _context.SalesStaffs.ToListAsync();
        }

        // GET
        [HttpGet("{id}")]
        public async Task<ActionResult<SalesStaff>> GetSalesStaff(int id)
        {
            var staff = await _context.SalesStaffs.FindAsync(id);

            if (staff == null)
            {
                return NotFound();
            }

            return staff;
        }

        // POST
        [HttpPost]
        public async Task<ActionResult<SalesStaff>> PostSalesStaff(SalesStaff staff)
        {
            _context.SalesStaffs.Add(staff);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSalesStaff), new { id = staff.StaffId }, staff);
        }

        // PUT
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSalesStaff(int id, SalesStaff staff)
        {
            if (id != staff.StaffId)
            {
                return BadRequest();
            }

            _context.Entry(staff).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalesStaffExists(id))
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
        public async Task<IActionResult> DeleteSalesStaff(int id)
        {
            var staff = await _context.SalesStaffs.FindAsync(id);
            if (staff == null)
            {
                return NotFound();
            }

            _context.SalesStaffs.Remove(staff);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SalesStaffExists(int id)
        {
            return _context.SalesStaffs.Any(e => e.StaffId == id);
        }
    }
}
