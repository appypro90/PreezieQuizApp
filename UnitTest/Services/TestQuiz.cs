using Models;
using System.Linq;
using NUnit.Framework;
using Services.ViewModels;
using System.Threading.Tasks;

namespace UnitTest.Services
{
    public class TestQuiz: QuizServicetestConfig
    {
        [SetUp]
        public new void Setup()
        {
        }

        [Test]
        public async Task ReturnAllQuestionsSuccessfully()
        {
            var result = await SystemUnderTest.GetAllQuestionsAnswers();
            Assert.AreEqual(4, result.Count());
        }

        [Test]
        public async Task ReturnTextQuestionSuccessfully()
        {
            var result = await SystemUnderTest.GetQuestionById(3);
            Assert.AreEqual("Your favourite destination", result.Qu);
        }

        [Test]
        public async Task ReturnMcqQuestionSuccessfully()
        {
            var result = await SystemUnderTest.GetQuestionById(2);
            Assert.AreEqual("Favourite Animal", result.Qu);
        }

        [Test]
        public async Task UpdateTextQuestionSuccessfully()
        {
            var qna = new AnswerVm
            {
                Id = 1,
                QuestionType = QuestionType.Text,
                TextAnswer = "Test Answer"
            };
            await SystemUnderTest.PatchAnswer(qna);
            Assert.AreEqual("Test Answer", (TestQuestions.First(q => q.Id == 1) as TextQuestion)?.TextAnswer);
        }

        [Test]
        public void UpdateMcqQuestionSuccessfully()
        {
            var qna = new AnswerVm
            {
                Id = 2,
                QuestionType = QuestionType.Mcq,
                McqAnswerId = 1
            };
            SystemUnderTest.PatchAnswer(qna);
            Assert.AreEqual("Cat", (TestQuestions.First(q => q.Id == 2) as McqQuestion)?.McqAnswer.Item2);
        }
    }
}
