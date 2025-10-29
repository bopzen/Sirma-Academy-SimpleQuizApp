using Microsoft.AspNetCore.Mvc;
using SimpleQuizApp.Data.Models;
using SimpleQuizApp.Models;
using SimpleQuizApp.Services;


namespace SimpleQuizApp.Controllers
{
    public class QuizController : Controller
    {
        private readonly QuizService _quizService;
        private const int QuestionCount = 10;

        public QuizController(QuizService quizService)
        {
            _quizService = quizService;
        }

        public IActionResult Start(string difficulty)
        {
            QuizState.ResetAnswers();
            QuizTime.ResetTimer();
            QuizTime.StartTimer();
            ViewBag.Difficulty = difficulty;
            var questions = _quizService.GetRandomQuestionsByDifficulty(difficulty, QuestionCount);

            QuizState.QuestionIds = questions.Select(q => q.Id).ToList();

            return RedirectToAction("Question", new { index = 0 });

        }

        public IActionResult Question(int index)
        {
            if (index >= QuestionCount)
            {
                return RedirectToAction("Result");
            }
            var questionId = QuizState.QuestionIds[index];
            var question = _quizService.GetQuestionByIdWithRandomOptions(questionId);

            var model = new QuestionViewModel
            {
                Id = question.Id,
                Question = question.Question,
                Difficulty = question.Difficulty,
                Options = question.Options.Select(o => o.Answer).ToList(),
                CurrentIndex = index + 1,
                TotalQuestions = QuestionCount,
                RemainingSeconds = QuizTime.GetRemainingSeconds()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Question(QuestionViewModel model)
        {
            QuizState.Answers[model.Id] = model.SelectedAnswer;

            if (model.CurrentIndex >= QuestionCount)
            {
                return RedirectToAction("Result");
            }

            return RedirectToAction("Question", new { index = model.CurrentIndex });
        }

        public IActionResult Result()
        {
            var totalSeconds = QuizTime.GetTotalSeconds();
            QuizTime.ResetTimer();
            var questions = _quizService.GetAllQuestions()
                .Where(q => QuizState.QuestionIds.Contains(q.Id))
                .OrderBy(q => QuizState.QuestionIds.IndexOf(q.Id))
                .ToList();

            var results = EvaluateAnswers(questions);
            int score = results.Where(r => r.IsCorrect == true).Count();

            var percentage = (double)score / (double)results.Count * 100;

            string comment = GenerateResultsComment(percentage);

            var model = new QuizResultViewModel
            {
                Results = results,
                Score = score,
                Percentage = percentage,
                Comment = comment,
                TotalQuestions = results.Count,
                TotalSeconds = totalSeconds
            };

            return View(model);
        }

        private string GenerateResultsComment(double percentage)
        {
            string comment;
            switch (percentage)
            {
                case < 50:
                    comment = "Don't worry, keep learning and you'll improve!";
                    break;
                case < 75:
                    comment = "Good effort! A little more practice and you'll get even better.";
                    break;
                case < 90:
                    comment = "Great job! You're very close to perfection.";
                    break;
                default:
                    comment = "Excellent work! You're a true genius!";
                    break;
            }
            return comment;
        }

        private List<QuizResultItem> EvaluateAnswers(List<QuizQuestion> questions)
        {
            var results = new List<QuizResultItem>();

            foreach (var q in questions)
            {
                if (!QuizState.Answers.TryGetValue(q.Id, out var userAnswer))
                {
                    userAnswer = "Not answered";
                }
                var correctAnswer = q.Options.First(o => o.IsCorrect).Answer;
                bool isAnswerCorrect = userAnswer == correctAnswer;
                results.Add(new QuizResultItem
                {
                    Question = q.Question,
                    UserAnswer = userAnswer,
                    CorrectAnswer = correctAnswer,
                    IsCorrect = isAnswerCorrect
                });
            }
            return results;
        }
    }
}
