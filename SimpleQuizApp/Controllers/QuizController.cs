using Microsoft.AspNetCore.Mvc;
using SimpleQuizApp.Data.Models;
using SimpleQuizApp.Models;
using SimpleQuizApp.Services;


namespace SimpleQuizApp.Controllers
{
    public class QuizController : Controller
    {
        private readonly QuizService _quizService;
        private const string QuestionIdsKey = "QuestionIds";
        private const int QuestionCount = 10;

        public QuizController(QuizService quizService)
        {
            _quizService = quizService;
        }

        public IActionResult Start(string difficulty)
        {
            ClearSession();
            ViewBag.Difficulty = difficulty;
            var questions = _quizService.GetRandomQuestionsByDifficulty(difficulty, QuestionCount);

            SaveQuestionIdsToSession(questions);

            return RedirectToAction("Question", new { index = 0 });

        }

        public IActionResult Question(int index)
        {
            var questionIds = GetQuestionIdsFromSession();

            if (index >= QuestionCount)
            {
                return RedirectToAction("Result");
            }

            var question = _quizService.GetQuestionByIdWithRandomOptions(questionIds[index]);

            var model = new QuestionViewModel
            {
                Id = question.Id,
                Question = question.Question,
                Difficulty = question.Difficulty,
                Options = question.Options.Select(o => o.Answer).ToList(),
                CurrentIndex = index + 1,
                TotalQuestions = QuestionCount
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Question(QuestionViewModel model)
        {
            SaveAnswerToSession(model.Id, model.SelectedAnswer);

            if (model.CurrentIndex >= QuestionCount)
            {
                return RedirectToAction("Result");
            }

            return RedirectToAction("Question", new { index = model.CurrentIndex });
        }

        public IActionResult Result()
        {
            var questionIds = GetQuestionIdsFromSession();
            var questions = _quizService.GetAllQuestions()
                .Where(q => questionIds.Contains(q.Id))
                .OrderBy(q => questionIds.IndexOf(q.Id))
                .ToList();

            var results = new List<QuizResultItem>();
            int score = 0;

            foreach (var q in questions)
            {
                var userAnswer = GetAnswerFromSession(q.Id);
                var correctAnswer = q.Options.First(o => o.IsCorrect).Answer;
                bool isAnswerCorrect = userAnswer == correctAnswer;

                if (isAnswerCorrect)
                {
                    score++;
                }

                results.Add(new QuizResultItem
                {
                    Question = q.Question,
                    UserAnswer = userAnswer,
                    CorrectAnswer = correctAnswer,
                    IsCorrect = isAnswerCorrect
                });
            }

            var percentage = (double)score / (double)results.Count * 100;
            string comment;
            switch (percentage)
            {
                case < 50:
                    comment = "Needs Improvement";
                    break;
                case < 75:
                    comment = "Good Effort";
                    break;
                case < 90:
                    comment = "Great Job";
                    break;
                default:
                    comment = "Excellent!";
                    break;
            }

            var model = new QuizResultViewModel
            {
                Results = results,
                Score = score,
                Percentage = percentage,
                Comment = comment,
                TotalQuestions = results.Count
            };

            return View(model);
        }


        private void ClearSession()
        {
            HttpContext.Session.Clear();
        }

        private void SaveQuestionIdsToSession(List<QuizQuestion> questions)
        {
            HttpContext.Session.SetString(QuestionIdsKey, string.Join(",", questions.Select(q => q.Id)));
        }
        private List<int> GetQuestionIdsFromSession()
        {
            var questionIds = HttpContext.Session.GetString(QuestionIdsKey)
                .Split(',')
                .Select(int.Parse)
                .ToList();
            return questionIds;
        }

        private void SaveAnswerToSession(int id, string selectedAnswe)
        {
            HttpContext.Session.SetString($"Answer_{id}", selectedAnswe);
        }

        private string GetAnswerFromSession(int id)
        {
            return HttpContext.Session.GetString($"Answer_{id}");
        }
    }
}
