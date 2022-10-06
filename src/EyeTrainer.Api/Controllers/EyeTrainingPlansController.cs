using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EyeTrainer.Api.Data;
using EyeTrainer.Api.Models;

namespace EyeTrainer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EyeTrainingPlansController : ControllerBase
    {
        private readonly EyeTrainerApiContext _context;

        public EyeTrainingPlansController(EyeTrainerApiContext context)
        {
            _context = context;
        }

        // GET: api/EyeTrainingPlans
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EyeTrainingPlan>>> GetEyeTrainingPlan()
        {
            return await _context.EyeTrainingPlan.ToListAsync();
        }

        // GET: api/EyeTrainingPlans/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EyeTrainingPlan>> GetEyeTrainingPlan(int id)
        {
            var eyeTrainingPlan = await _context.EyeTrainingPlan.FindAsync(id);

            if (eyeTrainingPlan == null)
            {
                return NotFound();
            }

            return eyeTrainingPlan;
        }

        // PUT: api/EyeTrainingPlans/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEyeTrainingPlan(int id, EyeTrainingPlan eyeTrainingPlan)
        {
            if (id != eyeTrainingPlan.Id)
            {
                return BadRequest();
            }

            _context.Entry(eyeTrainingPlan).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EyeTrainingPlanExists(id))
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

        // POST: api/EyeTrainingPlans
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EyeTrainingPlan>> PostEyeTrainingPlan(EyeTrainingPlan eyeTrainingPlan)
        {
            _context.EyeTrainingPlan.Add(eyeTrainingPlan);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEyeTrainingPlan", new { id = eyeTrainingPlan.Id }, eyeTrainingPlan);
        }

        // DELETE: api/EyeTrainingPlans/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEyeTrainingPlan(int id)
        {
            var eyeTrainingPlan = await _context.EyeTrainingPlan.FindAsync(id);
            if (eyeTrainingPlan == null)
            {
                return NotFound();
            }

            _context.EyeTrainingPlan.Remove(eyeTrainingPlan);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EyeTrainingPlanExists(int id)
        {
            return _context.EyeTrainingPlan.Any(e => e.Id == id);
        }
    }
}
