namespace SimpleQuizApp.Services
{
    public static class QuizState
    {
        public static List<int> QuestionIds {  get; set; } = new List<int>();
        public static Dictionary<int, string> Answers { get; set; } = new Dictionary<int, string>();

        public static void ResetAnswers()
        {
            QuestionIds.Clear();
            Answers.Clear();
        }
    }
}
