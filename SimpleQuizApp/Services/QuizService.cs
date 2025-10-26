using SimpleQuizApp.Data;
using SimpleQuizApp.Data.Models;

namespace SimpleQuizApp.Services
{
    public class QuizService
    {
        private readonly QuizRepository _data;

        public QuizService(QuizRepository data)
        {
            _data = data;
        }

        public List<QuizQuestion> GetAllQuestions()
        {
            return _data.GetAllQuestions();
        }

        public List<QuizQuestion> GetAllQuestionsByDifficulty(string difficulty)
        {
            return _data.GetAllQuestions().Where(q => q.Difficulty == difficulty).ToList();
        }

        public List<QuizQuestion> GetRandomQuestionsByDifficulty(string difficulty, int count)
        {
            var rnd = new Random();
            var allQuestions = _data.GetAllQuestions().Where(q =>q.Difficulty == difficulty).ToList();
            var randomQUestoins = allQuestions.OrderBy(q => rnd.Next()).Take(count).ToList();
            return randomQUestoins;
        }
    }
}
