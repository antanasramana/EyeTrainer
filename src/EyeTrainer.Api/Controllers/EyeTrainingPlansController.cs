using AutoMapper;
using EyeTrainer.Api.Constants;
using EyeTrainer.Api.Contracts.EyeTrainingPlan;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EyeTrainer.Api.Data;
using EyeTrainer.Api.Filters;
using EyeTrainer.Api.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Authorization;

namespace EyeTrainer.Api.Controllers
{
    [Route("api/Appointments/{appointmentId}/[controller]")]
    [ApiController]
    [AppointmentExistenceFilter]
    [Authorize(Policy = Policy.RequireRegularRights)]
    public class EyeTrainingPlansController : ControllerBase
    {
        private readonly EyeTrainerApiContext _context;
        private readonly IMapper _mapper;

        public EyeTrainingPlansController(EyeTrainerApiContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EyeTrainingPlanResponse>>> GetEyeTrainingPlans(int appointmentId)
        {
            var eyeTrainingPlans = await _context.EyeTrainingPlan
                .Where(plan => plan.AppointmentId == appointmentId)
                .ToListAsync();

            var eyeTrainingPlansResponse = _mapper.Map<IEnumerable<EyeTrainingPlanResponse>>(eyeTrainingPlans);

            return Ok(eyeTrainingPlansResponse);
        }

        [HttpGet("{eyeTrainingPlanId}")]
        public async Task<ActionResult<EyeTrainingPlanResponse>> GetEyeTrainingPlan(int appointmentId, int eyeTrainingPlanId)
        {
            var eyeTrainingPlan = await GetEyeTrainingPlanData(appointmentId, eyeTrainingPlanId);

            if (eyeTrainingPlan == null) return NotFound();

            var eyeTrainingPlanResponse = _mapper.Map<EyeTrainingPlanResponse>(eyeTrainingPlan);

            return eyeTrainingPlanResponse;
        }

        [HttpPatch("{eyeTrainingPlanId}")]
        [Authorize(Policy = Policy.RequireOwner)]
        public async Task<IActionResult> PatchEyeTrainingPlan(int appointmentId, int eyeTrainingPlanId, 
            JsonPatchDocument<PatchEyeTrainingPlanRequest> patchEyeTrainingPlanRequest)
        {
            var eyeTrainingPlan = await GetEyeTrainingPlanData(appointmentId, eyeTrainingPlanId);

            if (patchEyeTrainingPlanRequest == null) return BadRequest();
            if (eyeTrainingPlan == null) BadRequest("No such eye training plan with provided id");
            if (eyeTrainingPlan.Id != eyeTrainingPlanId) return BadRequest();

            var patchEyeTrainingPlan = _mapper.Map<PatchEyeTrainingPlanRequest>(eyeTrainingPlan);

            patchEyeTrainingPlanRequest.ApplyTo(patchEyeTrainingPlan);

            _mapper.Map<PatchEyeTrainingPlanRequest, EyeTrainingPlan>(patchEyeTrainingPlan, eyeTrainingPlan);

            _context.Update(eyeTrainingPlan);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<EyeTrainingPlanResponse>> PostEyeTrainingPlan(int appointmentId,
            PostEyeTrainingPlanRequest postEyeTrainingPlanRequest)
        {
            var eyeTrainingPlan = _mapper.Map<EyeTrainingPlan>(postEyeTrainingPlanRequest);
            eyeTrainingPlan.AppointmentId = appointmentId;

            _context.EyeTrainingPlan.Add(eyeTrainingPlan);
            await _context.SaveChangesAsync();

            var eyeTrainingPlanResponse = _mapper.Map<EyeTrainingPlanResponse>(eyeTrainingPlan);

            return CreatedAtAction("GetEyeTrainingPlan", new { appointmentId = appointmentId, eyeTrainingPlanId = eyeTrainingPlanResponse.Id }, eyeTrainingPlanResponse);
        }

        // DELETE: api/EyeTrainingPlans/5
        [HttpDelete("{eyeTrainingPlanId}")]
        [Authorize(Policy = Policy.RequireOwner)]
        public async Task<IActionResult> DeleteEyeTrainingPlan(int appointmentId, int eyeTrainingPlanId)
        {
            var eyeTrainingPlan = await GetEyeTrainingPlanData(appointmentId, eyeTrainingPlanId);

            if (eyeTrainingPlan == null)
            {
                return NotFound();
            }

            _context.EyeTrainingPlan.Remove(eyeTrainingPlan);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<EyeTrainingPlan> GetEyeTrainingPlanData(int appointmentId, int eyeTrainingPlanId)
        {
            var eyeTrainingPlan = await _context.EyeTrainingPlan
                .Where(plan => plan.AppointmentId == appointmentId)
                .FirstOrDefaultAsync(plan => plan.Id == eyeTrainingPlanId);

            return eyeTrainingPlan;
        }
    }
}
