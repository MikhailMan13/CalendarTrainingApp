using System.ComponentModel.DataAnnotations;

namespace CalendarTrainingApp.Models
{
    public class DailyProgress
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        
        [Display(Name = "100 Push-ups")]
        public bool PushUpsCompleted { get; set; }
        
        [Display(Name = "100 Squats")]
        public bool SquatsCompleted { get; set; }
        
        [Display(Name = "5000 Steps")]
        public bool StepsCompleted { get; set; }
    }
} 