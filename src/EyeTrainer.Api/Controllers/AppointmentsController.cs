using AutoMapper;
using EyeTrainer.Api.Constants;
using EyeTrainer.Api.Contracts.Appointment;
using EyeTrainer.Api.Data;
using EyeTrainer.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EyeTrainer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = Policy.RequireRegularRights)]
    public class AppointmentsController : ControllerBase
    {
        private readonly EyeTrainerApiContext _context;
        private readonly IMapper _mapper;

        public AppointmentsController(EyeTrainerApiContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppointmentResponse>>> GetAppointments()
        {
            var appointments = await _context.Appointment.ToListAsync();
            
            var appointmentResponses = _mapper.Map<IEnumerable<AppointmentResponse>>(appointments);

            return Ok(appointmentResponses);
        }

        [HttpGet("{appointmentId}")]
        public async Task<ActionResult<AppointmentResponse>> GetAppointment(int appointmentId)
        {
            var appointment = await _context.Appointment.FindAsync(appointmentId);

            if (appointment == null)
            {
                return NotFound();
            }

            var appointmentResponse = _mapper.Map<AppointmentResponse>(appointment);

            return appointmentResponse;
        }

        [HttpPatch("{appointmentId}")]
        [Authorize(Policy = Policy.RequireOwner)]
        public async Task<IActionResult> PatchAppointment(int appointmentId, 
            [FromBody] JsonPatchDocument<PatchAppointmentRequest> patchAppointmentRequest)
        {
            var appointment = await _context.Appointment.FindAsync(appointmentId);

            if (patchAppointmentRequest == null) return UnprocessableEntity();
            if (appointment == null) NotFound("No such appointment with provided id");

            var patchAppointment = _mapper.Map<PatchAppointmentRequest>(appointment);

            patchAppointmentRequest.ApplyTo(patchAppointment);

            _mapper.Map<PatchAppointmentRequest, Appointment>(patchAppointment, appointment);

            _context.Update(appointment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<AppointmentResponse>> PostAppointment(PostAppointmentRequest postAppointmentRequest)
        {
            var appointment = _mapper.Map<Appointment>(postAppointmentRequest);

            _context.Appointment.Add(appointment);
            await _context.SaveChangesAsync();

            var appointmentResponse = _mapper.Map<AppointmentResponse>(appointment);

            return CreatedAtAction("GetAppointment", new { appointmentId = appointmentResponse.Id }, appointmentResponse);
        }

        [HttpDelete("{appointmentId}")]
        [Authorize(Policy = Policy.RequireOwner)]
        public async Task<IActionResult> DeleteAppointment(int appointmentId)
        {
            var appointment = await _context.Appointment.FindAsync(appointmentId);
            if (appointment == null)
            {
                return NotFound();
            }

            _context.Appointment.Remove(appointment);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
