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

        public QuizQuestion GetQuestionByIdWithRandomOptions(int id)
        {
            var question = _data.GetAllQuestions().First(q => q.Id == id);
            var rnd = new Random();
            question.Options = question.Options.OrderBy(o => rnd.Next()).ToList();
            return question;
        }

        public List<QuizQuestion> GetRandomQuestionsByDifficulty(string difficulty, int count)
        {
            var rnd = new Random();
            var allQuestions = _data.GetAllQuestions().Where(q =>q.Difficulty == difficulty).ToList();
            var randomQuestions = allQuestions.OrderBy(q => rnd.Next()).Take(count).ToList();

            return randomQuestions;
        }
    }
}
