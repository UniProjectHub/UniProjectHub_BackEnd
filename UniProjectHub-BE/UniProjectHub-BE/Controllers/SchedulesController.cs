using Application.InterfaceServies;
using Application.ViewModels.ScheduleViewModel;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace UniProjectHub_BE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SchedulesController : ControllerBase
    {
        private readonly IScheduleService _scheduleService;

        public SchedulesController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        [HttpGet("all-schedules")]
        public async Task<IActionResult> GetAllSchedules()
        {
            var schedules = await _scheduleService.GetAllSchedulesAsync();
            return Ok(schedules);
        }

        /*  [HttpPost("create-schedule")]
          public async Task<IActionResult> CreateSchedule([FromBody] CreateScheduleViewModel createScheduleViewModel)
          {
              // Validate the model
              var validationResult = await _scheduleService.ValidateScheduleAsync(createScheduleViewModel);

              // Check if there are any validation errors
              if (!validationResult.IsValid)
              {
                  return BadRequest(new { Errors = validationResult.Errors.Select(e => e.ErrorMessage) });
              }

              // Proceed with creating the schedule
              var result = await _scheduleService.CreateScheduleAsync(createScheduleViewModel);
              return CreatedAtAction(nameof(GetScheduleById), new { id = result.Id }, result);
          }*/
        [HttpPost("create-schedule")]
        public async Task<IActionResult> CreateRecurringSchedules([FromBody] CreateScheduleViewModel createScheduleViewModel)
        {
            var schedules = await _scheduleService.CreateRecurringSchedulesAsync(createScheduleViewModel);
            return CreatedAtAction(nameof(GetAllSchedules), new { }, schedules);
        }

        [HttpPut("update-schedule/{id}")]
        public async Task<IActionResult> UpdateSchedule(int id, [FromBody] UpdateScheduleViewModel updateScheduleViewModel)
        {
            // Validate the model
            var validationResult = await _scheduleService.ValidateScheduleAsync(updateScheduleViewModel);

            // Check if there are any validation errors
            if (!validationResult.IsValid)
            {
                return BadRequest(new { Errors = validationResult.Errors.Select(e => e.ErrorMessage) });
            }

            // Proceed with updating the schedule
            var result = await _scheduleService.UpdateScheduleAsync(id, updateScheduleViewModel);

            // Check if the update was successful
            if (result == null)
            {
                return NotFound($"Schedule with ID {id} not found.");
            }

            return Ok(result);
        }

        [HttpDelete("delete-schedule/{id}")]
        public async Task<IActionResult> DeleteSchedule(int id)
        {
            try
            {
                await _scheduleService.DeleteScheduleAsync(id);
                return Ok("Delete success");
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Schedule with ID {id} not found.");
            }
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetSchedulesByUserId(string userId)
        {
            var result = await _scheduleService.GetSchedulesByUserIdAsync(userId);
            return Ok(result);
        }

        [HttpGet("get-schedule-by-id/{id}")]
        public async Task<IActionResult> GetScheduleById(int id)
        {
            var result = await _scheduleService.GetScheduleByIdAsync(id);
            if (result == null)
            {
                return NotFound($"Schedule with ID {id} not found.");
            }

            return Ok(result);
        }
    }
}
