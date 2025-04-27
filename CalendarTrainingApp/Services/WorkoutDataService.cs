using System.Text.Json;
using CalendarTrainingApp.Models;

namespace CalendarTrainingApp.Services
{
    public class WorkoutDataService
    {
        private readonly string _dataFilePath;
        private readonly IWebHostEnvironment _environment;

        public WorkoutDataService(IWebHostEnvironment environment)
        {
            _environment = environment;
            _dataFilePath = Path.Combine(_environment.ContentRootPath, "Data", "workoutdata.json");
            EnsureDataFileExists();
        }

        private void EnsureDataFileExists()
        {
            var directory = Path.GetDirectoryName(_dataFilePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            if (!File.Exists(_dataFilePath))
            {
                File.WriteAllText(_dataFilePath, "[]");
            }
        }

        public async Task<List<WorkoutTask>> GetAllTasksAsync()
        {
            var json = await File.ReadAllTextAsync(_dataFilePath);
            return JsonSerializer.Deserialize<List<WorkoutTask>>(json) ?? new List<WorkoutTask>();
        }

        public async Task SaveTasksAsync(List<WorkoutTask> tasks)
        {
            var json = JsonSerializer.Serialize(tasks);
            await File.WriteAllTextAsync(_dataFilePath, json);
        }

        public async Task<WorkoutTask?> GetTaskByIdAsync(int id)
        {
            var tasks = await GetAllTasksAsync();
            return tasks.FirstOrDefault(t => t.Id == id);
        }

        public async Task UpdateTaskAsync(WorkoutTask task)
        {
            var tasks = await GetAllTasksAsync();
            var existingTask = tasks.FirstOrDefault(t => t.Id == task.Id);
            if (existingTask != null)
            {
                existingTask.PushUpsCompleted = task.PushUpsCompleted;
                existingTask.SquatsCompleted = task.SquatsCompleted;
                existingTask.StepsCompleted = task.StepsCompleted;
                await SaveTasksAsync(tasks);
            }
        }

        public async Task AddTaskAsync(WorkoutTask task)
        {
            var tasks = await GetAllTasksAsync();
            task.Id = tasks.Count > 0 ? tasks.Max(t => t.Id) + 1 : 1;
            tasks.Add(task);
            await SaveTasksAsync(tasks);
        }
    }
} 