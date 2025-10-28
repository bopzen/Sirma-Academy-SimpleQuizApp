namespace SimpleQuizApp.Services
{
    public static class QuizTime
    {
        public static DateTime? StartTime { get; set; }
        public static TimeSpan Duration { get; set; } = TimeSpan.FromMinutes(10);

        public static void ResetTimer()
        {
            StartTime = null;
        }

        public static void StartTimer()
        {
            StartTime = DateTime.Now;
        }
        public static int GetRemainingSeconds()
        {
            if (StartTime == null)
            {
                return (int)Duration.TotalSeconds;
            }

            var elapsedTime = DateTime.Now - StartTime.Value;
            var remainingTime = Duration - elapsedTime;
            return remainingTime.TotalSeconds > 0 ? (int)remainingTime.TotalSeconds : 0;
        }

        public static int GetTotalSeconds()
        {
            if (StartTime == null) 
            {
                return 0;
            }

            var elapsedTime = DateTime.Now - StartTime.Value;

            return (int)elapsedTime.TotalSeconds;
        }
    }
}
