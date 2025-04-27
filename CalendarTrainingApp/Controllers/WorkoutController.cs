using Microsoft.AspNetCore.Mvc;
using CalendarTrainingApp.Models;
using CalendarTrainingApp.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CalendarTrainingApp.Controllers
{
    public class WorkoutController : Controller
    {
        private readonly WorkoutDataService _dataService;

        public WorkoutController(WorkoutDataService dataService)
        {
            _dataService = dataService;
        }

        public async Task<IActionResult> Index()
        {
            var tasks = await _dataService.GetAllTasksAsync();
            return View(tasks.OrderBy(t => t.Date));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTask(int id, int pushUps, int squats, int steps)
        {
            var task = await _dataService.GetTaskByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            task.PushUpsCompleted = pushUps;
            task.SquatsCompleted = squats;
            task.StepsCompleted = steps;

            await _dataService.UpdateTaskAsync(task);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask(DateTime date)
        {
            var task = new WorkoutTask
            {
                Date = date,
                PushUpsCompleted = 0,
                SquatsCompleted = 0,
                StepsCompleted = 0
            };

            await _dataService.AddTaskAsync(task);
            return RedirectToAction(nameof(Index));
        }
    }
} 