using Application.InterfaceServies;
using Application.ViewModels.ScheduleViewModel;
using Microsoft.AspNetCore.Mvc;

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
        [HttpPost("create-schedule")]
        public async Task<IActionResult> CreateSchedule([FromBody] ScheduleViewModel scheduleViewModel)
        {
            // Validate the model
            var validationResult = await _scheduleService.ValidateScheduleAsync(scheduleViewModel);

            // Check if there are any validation errors
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            // Proceed with creating the schedule
            var result = await _scheduleService.CreateScheduleAsync(scheduleViewModel);
            return Ok(result);
        }


        [HttpPut("update-schedule/{id}")]
        public async Task<IActionResult> UpdateSchedule(int id, [FromBody] ScheduleViewModel scheduleViewModel)
        {
            var result = await _scheduleService.UpdateScheduleAsync(scheduleViewModel, id);
            return Ok(result);
        }

        [HttpDelete("delete-schedule/{id}")]
        public async Task<IActionResult> DeleteSchedule(int id)
        {
            await _scheduleService.DeleteScheduleAsync(id);
            return NoContent();
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
            return Ok(result);
        }
    }
}
