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
    }
}
