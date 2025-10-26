namespace SimpleQuizApp.Data.Models
{
    public class QuizQuestion
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Difficulty { get; set; }
        public List<Option> Options { get; set; }
    }
}
