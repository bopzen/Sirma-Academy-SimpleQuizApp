using Microsoft.AspNetCore.Mvc;
using SimpleQuizApp.Data;
using SimpleQuizApp.Services;

namespace SimpleQuizApp.Controllers
{
    public class QuizController : Controller
    {
        private readonly QuizService _quizService;

        public QuizController(QuizService quizService)
        {
            _quizService = quizService;
        }

        public IActionResult Index()
        {
            var questions = _quizService.GetRandomQuestionsByDifficulty("easy", 10);
            return View(questions);
        }

        public IActionResult Start(string difficulty)
        {
            ViewBag.Difficulty = difficulty;
            var questions = _quizService.GetRandomQuestionsByDifficulty(difficulty, 10);
            return View(questions);
        }
    }
}
