using Services;
using Services.ViewModels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PreezieQuizApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IQuizService _quizService;

        public QuizController(IQuizService quizService)
        {
            _quizService = quizService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllQuestionsAnswers()
        {
            var questions = await _quizService.GetAllQuestionsAnswers();

            return Ok(questions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuestionById(int id)
        {
            var question = await _quizService.GetQuestionById(id);
            return Ok(question);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PutAnswer([FromBody]AnswerVm answer, int id)
        {
            await _quizService.PatchAnswer(answer);
            return Ok();
        }

    }
}