namespace SimpleQuizApp.Models
{
    public class QuestionViewModel
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Difficulty { get; set; }
        public List<string> Options { get; set; }
        public string SelectedAnswer { get; set; }
        public int CurrentIndex { get; set; }
        public int TotalQuestions { get; set; }
    }
}
