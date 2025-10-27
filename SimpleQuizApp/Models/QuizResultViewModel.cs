namespace SimpleQuizApp.Models
{
    public class QuizResultViewModel
    {
        public List<QuizResultItem> Results { get; set; } = new();
        public int Score { get; set; }
        public double Percentage { get; set; }
        public string Comment { get; set; }
        public int TotalQuestions { get; set; }
    }
}
