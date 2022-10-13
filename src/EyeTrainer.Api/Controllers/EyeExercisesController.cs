using AutoMapper;
using EyeTrainer.Api.Constants;
using EyeTrainer.Api.Contracts.EyeExercise;
using EyeTrainer.Api.Data;
using EyeTrainer.Api.Filters;
using EyeTrainer.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EyeTrainer.Api.Controllers
{
    [Route("api/Appointments/{appointmentId}/EyeTrainingPlans/{eyeTrainingPlanId}/[controller]")]
    [ApiController]
    [EyeTrainingPlanExistenceFilter]
    [Authorize(Policy = Policy.RequireRegularRights)]
    public class EyeExercisesController : ControllerBase
    {
        private readonly EyeTrainerApiContext _context;
        private readonly IMapper _mapper;

        public EyeExercisesController(EyeTrainerApiContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/EyeExercises
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EyeExerciseResponse>>> GetEyeExercise(int appointmentId, int eyeTrainingPlanId)
        {
            var eyeTrainingPlan = await GetEyeTrainingPlan(appointmentId, eyeTrainingPlanId);
            var eyeExercises = eyeTrainingPlan.EyeExercises;

            var eyeExercisesResponses = _mapper.Map<IEnumerable<EyeExerciseResponse>>(eyeExercises);

            return Ok(eyeExercisesResponses);
        }

        [HttpGet("{eyeExerciseId}")]
        public async Task<ActionResult<EyeExerciseResponse>> GetEyeExercise(int appointmentId, int eyeTrainingPlanId, int eyeExerciseId)
        {
            var eyeTrainingPlan = await GetEyeTrainingPlan(appointmentId, eyeTrainingPlanId);
            
            var eyeExercise = eyeTrainingPlan.EyeExercises
                .FirstOrDefault(e => e.Id == eyeExerciseId);
            if (eyeExercise == null) return NotFound();

            var eyeExerciseResponse = _mapper.Map<EyeExerciseResponse>(eyeExercise);

            return eyeExerciseResponse;
        }

        [HttpPatch("{eyeExerciseId}")]
        [Authorize(Policy = Policy.RequireOwner)]
        public async Task<IActionResult> PatchEyeExercise(int appointmentId, 
            int eyeTrainingPlanId, 
            int eyeExerciseId, 
            JsonPatchDocument<PatchEyeExerciseRequest> patchEyeExerciseRequest)
        {
            var eyeTrainingPlan = await GetEyeTrainingPlan(appointmentId, eyeTrainingPlanId);

            var eyeExercise = eyeTrainingPlan.EyeExercises
                .FirstOrDefault(e => e.Id == eyeExerciseId);
            if (eyeExercise == null) BadRequest("No such eye exercise with provided id");

            var patchEyeExercise = _mapper.Map<PatchEyeExerciseRequest>(eyeExercise);

            patchEyeExerciseRequest.ApplyTo(patchEyeExercise);

            _mapper.Map<PatchEyeExerciseRequest, EyeExercise>(patchEyeExercise, eyeExercise);

            _context.Update(eyeExercise);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<EyeExerciseResponse>> PostEyeExercise(
            int appointmentId,
            int eyeTrainingPlanId, 
            PostEyeExerciseRequest postEyeExerciseRequest)
        {
            var eyeExercise = _mapper.Map<EyeExercise>(postEyeExerciseRequest);

            var eyeTrainingPlan = await GetEyeTrainingPlan(appointmentId, eyeTrainingPlanId);

            eyeTrainingPlan.EyeExercises.Add(eyeExercise);

            _context.Update(eyeTrainingPlan);
            _context.EyeExercise.Add(eyeExercise);
            await _context.SaveChangesAsync();

            var eyeExerciseResponse = _mapper.Map<EyeExerciseResponse>(eyeExercise);

            return CreatedAtAction("GetEyeExercise", new { appointmentId = appointmentId, eyeTrainingPlanId = eyeTrainingPlanId, eyeExerciseId = eyeExercise.Id }, eyeExerciseResponse);
        }

        [HttpDelete("{eyeExerciseId}")]
        [Authorize(Policy = Policy.RequireOwner)]
        public async Task<IActionResult> DeleteEyeExercise(int appointmentId,
            int eyeTrainingPlanId,
            int eyeExerciseId)
        {
            var eyeExercise = await _context.EyeExercise.FindAsync(eyeExerciseId);
            if (eyeExercise == null)
            {
                return NotFound();
            }

            _context.EyeExercise.Remove(eyeExercise);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<EyeTrainingPlan> GetEyeTrainingPlan(int appointmentId, int eyeTrainingPlanId)
        {
            var appointment = await _context.Appointment
                .Include(app => app.EyeTrainingPlans)
                .ThenInclude(plan => plan.EyeExercises)
                .FirstAsync(app => app.Id == appointmentId);

            var eyeTrainingPlan = appointment.EyeTrainingPlans
                .FirstOrDefault(plan => plan.Id == eyeTrainingPlanId);
            return eyeTrainingPlan;
        }
    }
}
