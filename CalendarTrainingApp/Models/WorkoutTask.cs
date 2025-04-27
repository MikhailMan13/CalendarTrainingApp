using System;

namespace CalendarTrainingApp.Models
{
    public class WorkoutTask
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int PushUpsCompleted { get; set; }
        public int SquatsCompleted { get; set; }
        public int StepsCompleted { get; set; }
        public bool IsPushUpsCompleted => PushUpsCompleted >= 100;
        public bool IsSquatsCompleted => SquatsCompleted >= 100;
        public bool IsStepsCompleted => StepsCompleted >= 5000;
        public bool IsAllCompleted => IsPushUpsCompleted && IsSquatsCompleted && IsStepsCompleted;
    }
} 