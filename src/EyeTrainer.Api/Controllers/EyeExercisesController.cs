using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EyeTrainer.Api.Data;
using EyeTrainer.Api.Models;

namespace EyeTrainer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EyeExercisesController : ControllerBase
    {
        private readonly EyeTrainerApiContext _context;

        public EyeExercisesController(EyeTrainerApiContext context)
        {
            _context = context;
        }

        // GET: api/EyeExercises
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EyeExercise>>> GetEyeExercise()
        {
            return await _context.EyeExercise.ToListAsync();
        }

        // GET: api/EyeExercises/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EyeExercise>> GetEyeExercise(int id)
        {
            var eyeExercise = await _context.EyeExercise.FindAsync(id);

            if (eyeExercise == null)
            {
                return NotFound();
            }

            return eyeExercise;
        }

        // PUT: api/EyeExercises/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEyeExercise(int id, EyeExercise eyeExercise)
        {
            if (id != eyeExercise.Id)
            {
                return BadRequest();
            }

            _context.Entry(eyeExercise).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EyeExerciseExists(id))
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

        // POST: api/EyeExercises
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EyeExercise>> PostEyeExercise(EyeExercise eyeExercise)
        {
            _context.EyeExercise.Add(eyeExercise);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEyeExercise", new { id = eyeExercise.Id }, eyeExercise);
        }

        // DELETE: api/EyeExercises/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEyeExercise(int id)
        {
            var eyeExercise = await _context.EyeExercise.FindAsync(id);
            if (eyeExercise == null)
            {
                return NotFound();
            }

            _context.EyeExercise.Remove(eyeExercise);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EyeExerciseExists(int id)
        {
            return _context.EyeExercise.Any(e => e.Id == id);
        }
    }
}
