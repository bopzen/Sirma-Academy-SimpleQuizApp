using Newtonsoft.Json;
using SimpleQuizApp.Data.Models;

namespace SimpleQuizApp.Data
{
    public class QuizRepository
    {
        private readonly string _filePath;

        public QuizRepository(IConfiguration configuration)
        {
             _filePath = configuration["FilePath"] ?? throw new Exception("Quiz file path not configured.");
        }

        public List<QuizQuestion> GetAllQuestions()
        {
            if (!File.Exists(_filePath))
                throw new FileNotFoundException($"Quiz file not found at {_filePath}");

            var json = File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<List<QuizQuestion>>(json);
        }
    }
}
